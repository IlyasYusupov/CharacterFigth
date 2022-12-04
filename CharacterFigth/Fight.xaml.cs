using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CharacterFigth
{
    /// <summary>
    /// Логика взаимодействия для Fight.xaml
    /// </summary>
    public partial class Fight : Window
    {
        Character firstCharacter;
        Character secondCharacter;
        bool isFighting;
        int cnt = -1;
        double xpBoost = 0;
        double xpBoost2 = 0;


        public Fight()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(firstCharacter != null)
            {
                var dialogResult = MessageBox.Show("У вас уже есть боец, вы точно хотите создать нового?","Предупреждение!",MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.No) 
                {
                    return;
                }
            }
            MainWindow wind = new MainWindow();
            wind.ShowDialog();
            firstCharacter = wind.character;
            FillFirstCharacter(firstCharacter);

        }

        private void ButtonCreate2_Click(object sender, RoutedEventArgs e)
        {
            if (secondCharacter != null)
            {
                var dialogResult = MessageBox.Show("У вас уже есть боец, вы точно хотите создать нового?", "Предупреждение!", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.No)
                {
                    return;
                }
            }
            MainWindow wind = new MainWindow();
            wind.ShowDialog();
            secondCharacter = wind.character;
            FillSecondCharacter(secondCharacter);

        }

        private void FillFirstCharacter(Character character)
        {
            if(firstCharacter != null) 
            {
                tbName.Text = character.Name;
                pbHP.Maximum = character.HealthPoint;
                pbHP.Value = character.HealthPoint;
                HP.Text = $"HP: {character.HealthPoint}";
            }
        }

        private void FillSecondCharacter(Character character)
        {
            if (secondCharacter != null)
            {
                tbName2.Text = character.Name;
                pbHP2.Maximum = character.HealthPoint;
                pbHP2.Value = character.HealthPoint;
                HP2.Text = $"HP: {character.HealthPoint}";
            }
        }

        private void CharacteristicFirst_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wind = new MainWindow();
            if(firstCharacter != null)
            {
                wind.Fill(firstCharacter);
                wind.ShowDialog();
                if (wind.character != null)
                {
                    firstCharacter = wind.character;
                    FillFirstCharacter(firstCharacter);
                }
            }
        }

        private void CharacteristicSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wind = new MainWindow();
            if (secondCharacter != null)
            {
                wind.Fill(secondCharacter);
                wind.ShowDialog();
                if(wind.character != null)
                {
                    secondCharacter = wind.character;
                    FillSecondCharacter(secondCharacter);
                }
            }
        }

        double Evasion;
        double Evasion2;
        double CriticalHit;
        double CriticalHit2;
        double MannaPool;
        double MannaPool2;

        private void btnFight_Click(object sender, RoutedEventArgs e)
        {
            if(firstCharacter != null && secondCharacter != null) 
            {
                cnt = -1;
                isFighting = true;
                Create.Visibility = Visibility.Hidden;
                Create2.Visibility = Visibility.Hidden;
                Characteristic.Visibility = Visibility.Hidden;
                Characteristic2.Visibility = Visibility.Hidden;
                btnFight.Visibility = Visibility.Hidden;
                btnPhysAttack.Visibility = Visibility.Visible;
                btnMagAttack.Visibility = Visibility.Visible;
                btnSelfHeal.Visibility = Visibility.Visible;
                listChat.Visibility = Visibility.Visible;
                chat.Visibility = Visibility.Visible;
                tbWhoAttack.Visibility = Visibility.Visible;
                tbWhoAttack.Text = $"Атакует боец {firstCharacter.Name}";

                if((firstCharacter.Dexterity - secondCharacter.Dexterity) > 0)
                {
                    Evasion = (firstCharacter.Dexterity - secondCharacter.Dexterity) * 0.1 * 100 ;
                    tbEvasion.Text = $"{(int)Evasion} %";
                }
                else
                {
                    Evasion = 0;
                    tbEvasion.Text = "0 %";
                }

                if ((secondCharacter.Dexterity - firstCharacter.Dexterity)  > 0)
                {
                    Evasion2 = (secondCharacter.Dexterity - firstCharacter.Dexterity) * 0.1 * 100;
                    tbEvasion2.Text = $"{(int)Evasion2} %";
                }
                else
                {
                    Evasion2 = 0;
                    tbEvasion2.Text = "0 %";
                }

                if ((firstCharacter.Luck - secondCharacter.Luck) > 0)
                {
                    CriticalHit = (firstCharacter.Luck - secondCharacter.Luck) * 0.5 * 10;
                    tbCriticalHit.Text = $"{(int)CriticalHit} %";
                }
                else
                {
                    CriticalHit = 0;
                    tbCriticalHit.Text = $"{(int)CriticalHit} %";
                }

                if ((secondCharacter.Luck - firstCharacter.Luck) > 0)
                {
                    CriticalHit2 = (secondCharacter.Luck - firstCharacter.Luck) * 0.5 * 10;
                    tbCriticalHit2.Text = $"{(int)CriticalHit2} %";
                }
                else
                {
                    CriticalHit2 = 0;
                    tbCriticalHit2.Text = $"{(int)CriticalHit2} %";
                }

                tbManaPool.Text = firstCharacter.ManaPool.ToString();
                tbManaPool2.Text = secondCharacter.ManaPool.ToString();
            }
            else
            {
                MessageBox.Show($"Создайте двух бойцов!");
            }
        }

        private void btnPhysAttack_Click(object sender, RoutedEventArgs e)
        {
            if((pbHP.Value != 0 && pbHP2.Value != 0) && isFighting) 
            {
                if (cnt == -1)
                {
                    if (!isEvasion())
                    {
                        if (!isBlocked())
                        {
                            if (isCriticalHit())
                            {
                                if (pbHP2.Value - firstCharacter.PhysicalAttake * 2 <= 0)
                                {
                                    xpBoost += pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value);
                                    listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс критический урон: {pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value)}");
                                    pbHP2.Value = 0;
                                    HP2.Text = $"HP: {pbHP2.Value}";
                                    isWinner(-1);
                                    isLosеr(1);
                                }
                                else
                                {
                                    xpBoost += firstCharacter.PhysicalAttake * 2;
                                    pbHP2.Value = pbHP2.Value - firstCharacter.PhysicalAttake * 2;
                                    listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс критический урон: {firstCharacter.PhysicalAttake * 2}");
                                    HP2.Text = $"HP: {pbHP2.Value}";
                                }
                            }
                            else
                            {
                                if (pbHP2.Value - firstCharacter.PhysicalAttake <= 0)
                                {
                                    xpBoost += pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value);
                                    listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс урон: {pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value)}");
                                    pbHP2.Value = 0;
                                    HP2.Text = $"HP: {pbHP2.Value}";
                                    isWinner(-1);
                                    isLosеr(1);
                                }
                                else
                                {
                                    xpBoost += firstCharacter.PhysicalAttake;
                                    pbHP2.Value = pbHP2.Value - firstCharacter.PhysicalAttake;
                                    listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс урон: {firstCharacter.PhysicalAttake}");
                                    HP2.Text = $"HP: {pbHP2.Value}";
                                }
                            }
                        }
                        else
                        {
                            listChat.Items.Add($"- Боец {firstCharacter.Name} не смог пробить блок Бойцу{secondCharacter.Name}");
                        }
                    }
                    else
                    {
                        listChat.Items.Add($"- Боец {firstCharacter.Name} промахнулся по Бойцу{secondCharacter.Name}");
                    }
                }
                else if(cnt == 1)
                {
                    if (!isEvasion())
                    {
                        if (!isBlocked())
                        {
                            if (isCriticalHit())
                            {
                                if (pbHP.Value - secondCharacter.PhysicalAttake * 2 <= 0)
                                {
                                    xpBoost2 += pbHP.Maximum - (pbHP.Maximum - pbHP.Value);
                                    listChat.Items.Add($"- {secondCharacter.Name} нанёс критический урон: {pbHP.Maximum - (pbHP.Maximum - pbHP.Value)}");
                                    pbHP.Value = 0;
                                    HP.Text = $"HP: {pbHP.Value}";
                                    isWinner(1);
                                    isLosеr(-1);
                                }
                                else
                                {
                                    xpBoost2 += secondCharacter.PhysicalAttake * 2;
                                    pbHP.Value = pbHP.Value - secondCharacter.PhysicalAttake * 2;
                                    listChat.Items.Add($"- {secondCharacter.Name} нанёс критический урон: {secondCharacter.PhysicalAttake * 2}");
                                    HP.Text = $"HP: {pbHP.Value}";
                                }
                            }
                            else
                            {
                                if (pbHP.Value - secondCharacter.PhysicalAttake <= 0)
                                {
                                    xpBoost2 += pbHP.Maximum - (pbHP.Maximum - pbHP.Value);
                                    listChat.Items.Add($"- {secondCharacter.Name} нанёс урон: {pbHP.Maximum - (pbHP.Maximum - pbHP.Value)}");
                                    pbHP.Value = 0;
                                    HP.Text = $"HP: {pbHP.Value}";
                                    isWinner(1);
                                    isLosеr(-1);
                                }
                                else
                                {
                                    xpBoost2 += secondCharacter.PhysicalAttake;
                                    pbHP.Value = pbHP.Value - secondCharacter.PhysicalAttake;
                                    listChat.Items.Add($"- {secondCharacter.Name} нанёс урон: {secondCharacter.PhysicalAttake}");
                                    HP.Text = $"HP: {pbHP.Value}";
                                }
                            }
                        }
                        else
                        {
                            listChat.Items.Add($"- {secondCharacter.Name} не смог пробить блок {firstCharacter.Name}");
                        }
                    }
                    else
                    {
                        listChat.Items.Add($"- {secondCharacter.Name} промахнулся по {firstCharacter.Name}");
                    }
                }
                cnt *= -1;
                if (cnt == -1)
                {
                    tbWhoAttack.Text = $"Атакует боец {firstCharacter.Name}";
                }
                else
                {
                    tbWhoAttack.Text = $"Атакует боец {secondCharacter.Name}";
                }
            }
        }

        private void btnMagAttack_Click(object sender, RoutedEventArgs e)
        {
            if ((pbHP.Value != 0 && pbHP2.Value != 0) && isFighting)
            {
                if (cnt == -1)
                {
                    if (MannaEst())
                    {
                        if (!isEvasion())
                        {
                            if (!isBlocked())
                            {
                                if (isCriticalHit())
                                {
                                    if (pbHP2.Value - firstCharacter.MagicalAttake * 2 <= 0)
                                    {
                                        xpBoost += pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value);
                                        listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс критический магический урон: {pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value)}");
                                        pbHP2.Value = 0;
                                        HP2.Text = $"HP: {pbHP2.Value}";
                                        isWinner(-1);
                                        isLosеr(1);
                                    }
                                    else
                                    {
                                        xpBoost += firstCharacter.MagicalAttake * 2;
                                        pbHP2.Value = pbHP2.Value - firstCharacter.MagicalAttake * 2;
                                        listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс критический магический урон: {firstCharacter.MagicalAttake * 2}");
                                        HP2.Text = $"HP: {pbHP2.Value}";
                                    }
                                }
                                else
                                {
                                    if (pbHP2.Value - firstCharacter.MagicalAttake <= 0)
                                    {
                                        xpBoost += pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value);
                                        listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс магический урон: {pbHP2.Maximum - (pbHP2.Maximum - pbHP2.Value)}");
                                        pbHP2.Value = 0;
                                        HP2.Text = $"HP: {pbHP2.Value}";
                                        isWinner(-1);
                                        isLosеr(1);
                                    }
                                    else
                                    {
                                        xpBoost += firstCharacter.MagicalAttake;
                                        pbHP2.Value = pbHP2.Value - firstCharacter.MagicalAttake;
                                        listChat.Items.Add($"- Боец {firstCharacter.Name} нанёс магический урон: {firstCharacter.MagicalAttake}");
                                        HP2.Text = $"HP: {pbHP2.Value}";
                                    }
                                }
                            }
                            else
                            {
                                listChat.Items.Add($"- Боец {firstCharacter.Name} не смог пробить блок Бойцу {secondCharacter.Name}");
                            }
                        }
                        else
                        {
                            listChat.Items.Add($"- Боец {firstCharacter.Name} промахнулся по Бойцу {secondCharacter.Name}");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Не хватает маны!");
                        return;
                    }
                }
                else if (cnt == 1)
                {
                    if (MannaEst())
                    {
                        if (!isEvasion())
                        {
                            if (!isBlocked())
                            {
                                if (isCriticalHit())
                                {
                                    if (pbHP.Value - secondCharacter.MagicalAttake * 2 <= 0)
                                    {
                                        xpBoost2 += pbHP.Maximum - (pbHP.Maximum - pbHP.Value);
                                        listChat.Items.Add($"- {secondCharacter.Name} нанёс критический урон: {pbHP.Maximum - (pbHP.Maximum - pbHP.Value)}");
                                        pbHP.Value = 0;
                                        HP.Text = $"HP: {pbHP.Value}";
                                        isWinner(1);
                                        isLosеr(-1);
                                    }
                                    else
                                    {
                                        xpBoost2 += secondCharacter.MagicalAttake * 2;
                                        pbHP.Value = pbHP.Value - secondCharacter.MagicalAttake * 2;
                                        listChat.Items.Add($"- {secondCharacter.Name} нанёс магический критический урон: {secondCharacter.MagicalAttake * 2}");
                                        HP.Text = $"HP: {pbHP.Value}";
                                    }
                                }
                                else
                                {
                                    if (pbHP.Value - secondCharacter.MagicalAttake <= 0)
                                    {
                                        xpBoost2 += pbHP.Maximum - (pbHP.Maximum - pbHP.Value);
                                        listChat.Items.Add($"- {secondCharacter.Name} нанёс магический урон: {pbHP.Maximum - (pbHP.Maximum - pbHP.Value)}");
                                        pbHP.Value = 0;
                                        HP.Text = $"HP: {pbHP.Value}";
                                        isWinner(1);
                                        isLosеr(-1);
                                    }
                                    else
                                    {
                                        xpBoost2 += secondCharacter.MagicalAttake;
                                        pbHP.Value = pbHP.Value - secondCharacter.MagicalAttake;
                                        listChat.Items.Add($"- {secondCharacter.Name} нанёс магический урон: {secondCharacter.MagicalAttake}");
                                        HP.Text = $"HP: {pbHP.Value}";
                                    }
                                }
                            }
                            else
                            {
                                listChat.Items.Add($"- {secondCharacter.Name} не смог пробить блок {firstCharacter.Name}");
                            }
                        }
                        else
                        {
                            listChat.Items.Add($"- {secondCharacter.Name} промахнулся по {firstCharacter.Name}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Не хватает маны!");
                    return;
                }
                cnt *= -1;
                if (cnt == -1)
                {
                    tbWhoAttack.Text = $"Атакует боец {firstCharacter.Name}";
                }
                else
                {
                    tbWhoAttack.Text = $"Атакует боец {secondCharacter.Name}";
                }
            }
        }

        private void btnSelfHeal_Click(object sender, RoutedEventArgs e)
        {
            if(isFighting)
            {
                if (cnt == -1)
                {
                    if (pbHP.Value != pbHP.Maximum)
                    {
                        if (firstCharacter.Intellect >= 5)
                        {
                            int heal = (int)(pbHP.Maximum - pbHP.Value);
                            if((heal * 0.5) <= double.Parse(tbManaPool.Text))
                            {
                                pbHP.Value = pbHP.Maximum;
                                HP.Text = $"HP: {pbHP.Maximum}";
                                tbManaPool.Text = $"{double.Parse(tbManaPool.Text) - (heal * 0.5)}";
                                listChat.Items.Add($"- Боец {firstCharacter.Name} восстановил {heal} здоровья");
                            }
                            else if((heal * 0.5) >= double.Parse(tbManaPool.Text) && double.Parse(tbManaPool.Text) > 0)
                            {
                                HP.Text = $"HP: {pbHP.Value + double.Parse(tbManaPool.Text) * 2}";
                                pbHP.Value += double.Parse(tbManaPool.Text) * 2;
                                listChat.Items.Add($"- Боец {firstCharacter.Name} восстановил {double.Parse(tbManaPool.Text) * 2} здоровья");
                                tbManaPool.Text = $"{double.Parse(tbManaPool.Text) - double.Parse(tbManaPool.Text)}";
                            }
                            else
                            {
                                MessageBox.Show("Не хватает маны");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Требуется 5 уровень интелекта");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Здоровье уже полное!");
                        return;
                    }
                }
                else if(cnt == 1)
                {
                    if (pbHP2.Value != pbHP2.Maximum)
                    {
                        if (secondCharacter.Intellect >= 5)
                        {
                            int heal = (int)(pbHP2.Maximum - pbHP2.Value);
                            if ((heal * 0.5) <= double.Parse(tbManaPool2.Text))
                            {
                                pbHP2.Value = pbHP2.Maximum;
                                HP2.Text = $"HP: {pbHP2.Maximum}";
                                tbManaPool2.Text = $"{double.Parse(tbManaPool2.Text) - (heal * 0.5)}";
                                listChat.Items.Add($"- Боец {secondCharacter.Name} восстановил {heal} здоровья");
                            }
                            else if ((heal * 0.5) >= double.Parse(tbManaPool2.Text) && double.Parse(tbManaPool2.Text) > 1)
                            {
                                HP2.Text = $"HP: {pbHP.Value + double.Parse(tbManaPool2.Text) * 2}";
                                pbHP.Value += double.Parse(tbManaPool2.Text) * 2;
                                listChat.Items.Add($"- Боец {secondCharacter.Name} восстановил {double.Parse(tbManaPool2.Text) * 2} здоровья");
                                tbManaPool2.Text = $"{double.Parse(tbManaPool2.Text) - double.Parse(tbManaPool2.Text)}";
                            }
                            else
                            {
                                MessageBox.Show("Не хватает маны");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Требуется 5 уровень интелекта");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Здоровье уже полное!");
                        return;
                    }
                }
                cnt *= -1;
                if (cnt == -1)
                {
                    tbWhoAttack.Text = $"Атакует боец {firstCharacter.Name}";
                }
                else
                {
                    tbWhoAttack.Text = $"Атакует боец {secondCharacter.Name}";
                }
            }
        }

        private bool isEvasion()
        {
            var rnd = new Random();
            int prom = rnd.Next(1, 100);
            if(cnt == -1)
            {
                if (prom >= 1 && prom <= (int)Evasion2)
                {
                    return true;
                }
                else
                    return false;
            }
            if (cnt == 1)
            {
                if (prom >= 1 && prom <= (int)Evasion)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        private bool isBlocked()
        {
            var rnd = new Random();
            int prom = rnd.Next(1, 100);
            if (cnt == -1)
            {
                if(firstCharacter.Strength == secondCharacter.Strength)
                {
                    return false;
                }
                else if (APPointText == BPPointText2)
                {
                    if (prom >= 1 && prom <= (firstCharacter.Strength - secondCharacter.Strength) * 0.05 * 100)
                    {
                        return false;
                    }
                    else
                        return true;
                }
            }
            if (cnt == 1)
            {
                if (secondCharacter.Strength == firstCharacter.Strength)
                {
                    return false;
                }
                else if (APPointText2 == BPPointText)
                {
                    if (prom >= 1 && prom <= (secondCharacter.Strength - firstCharacter.Strength))
                    {
                        return false;
                    }
                    else
                        return true;
                }
            }
            return false;
        }

        private bool isCriticalHit()
        {
            var rnd = new Random();
            int prom = rnd.Next(1, 100);
            if (cnt == -1)
            {
                if (prom >= 1 && prom <= CriticalHit)
                {
                    return true;
                }
                else
                    return false;
            }
            if (cnt == 1)
            {
                if (prom >= 1 && prom <= CriticalHit2)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        private void isWinner(int id)
        {
            if(id == -1)
            {
                MessageBox.Show($"Winner {firstCharacter.Name} ");
                firstCharacter.XP += (int)xpBoost * 3 * secondCharacter.LVL;
            }
            if (id == 1)
            {
                MessageBox.Show($"Winner {secondCharacter.Name} ");
                secondCharacter.XP += (int)xpBoost2 * 3 * firstCharacter.LVL;
            }
        }

        int skillGive2 = -1;
        int skillGive3 = -1;
        int skillGive4 = -1;
        int skillGive5 = -1;
        int skillGive6 = -1;

        int skillGive22 = -1;
        int skillGive32 = -1;
        int skillGive42 = -1;
        int skillGive52 = -1;
        int skillGive62 = -1;

        private void isLosеr(int id)
        {
            if (id == -1)
            {
                firstCharacter.XP += (int)xpBoost * 3;
            }

            if (firstCharacter.XP >= Character.LVLXP[2] && firstCharacter.XP < Character.LVLXP[3])
            {
                firstCharacter.LVL = 2;
                if(skillGive2 == -1)
                {
                    firstCharacter.SkillPoints += 2;
                    skillGive2 *= -1;
                }
            }

            else if (firstCharacter.XP >= Character.LVLXP[3] && firstCharacter.XP < Character.LVLXP[4])
            {
                firstCharacter.LVL = 3;
                if (skillGive3 == -1)
                {
                    firstCharacter.SkillPoints += 2;
                    skillGive3 *= -1;
                }
            }

            else if (firstCharacter.XP >= Character.LVLXP[4] && firstCharacter.XP < Character.LVLXP[5])
            {
                firstCharacter.LVL = 4;
                if (skillGive4 == -1)
                {
                    firstCharacter.SkillPoints += 3;
                    skillGive4 *= -1;
                }
            }

            else if (firstCharacter.XP >= Character.LVLXP[5] && firstCharacter.XP < Character.LVLXP[6])
            {
                firstCharacter.LVL = 5;
                if (skillGive5 == -1)
                {
                    firstCharacter.SkillPoints += 2;
                    skillGive5 *= -1;
                }
            }

            else if (firstCharacter.XP >= Character.LVLXP[6])
            {
                firstCharacter.LVL = 6;
                if (skillGive6 == -1)
                {
                    firstCharacter.SkillPoints += 5;
                    skillGive6 *= -1;
                }
            }

            if (id == 1)
            {
                secondCharacter.XP += (int)xpBoost2 * 3;
            }

            if (secondCharacter.XP >= Character.LVLXP[2] && secondCharacter.XP < Character.LVLXP[3])
            {
                secondCharacter.LVL = 2;
                if (skillGive22 == -1)
                {
                    secondCharacter.SkillPoints += 2;
                    skillGive22 *= -1;
                }
            }

            else if (secondCharacter.XP >= Character.LVLXP[3] && secondCharacter.XP < Character.LVLXP[4])
            {
                secondCharacter.LVL = 3;
                if (skillGive32 == -1)
                {
                    secondCharacter.SkillPoints += 2;
                    skillGive32 *= -1;
                }
            }

            else if (secondCharacter.XP >= Character.LVLXP[4] && secondCharacter.XP < Character.LVLXP[5])
            {
                secondCharacter.LVL = 4;
                if (skillGive42 == -1)
                {
                    secondCharacter.SkillPoints += 3;
                    skillGive42 *= -1;
                }
            }

            else if (secondCharacter.XP >= Character.LVLXP[5] && secondCharacter.XP < Character.LVLXP[6])
            {
                secondCharacter.LVL = 5;
                if (skillGive52 == -1)
                {
                    secondCharacter.SkillPoints += 2;
                    skillGive52 *= -1;
                }
            }

            else if (secondCharacter.XP >= Character.LVLXP[6])
            {
                secondCharacter.LVL = 6;
                if (skillGive62 == -1)
                {
                    secondCharacter.SkillPoints += 5;
                    skillGive62 *= -1;
                }
            }

            pbHP.Value = pbHP.Maximum;
            pbHP2.Value = pbHP2.Maximum;
            HP.Text = $"HP: {pbHP.Maximum}";
            HP2.Text = $"HP: {pbHP.Maximum}";
            isFighting = false;
            Create.Visibility = Visibility.Visible;
            Create2.Visibility = Visibility.Visible;
            Characteristic.Visibility = Visibility.Visible;
            Characteristic2.Visibility = Visibility.Visible;
            btnFight.Visibility = Visibility.Visible;
            btnPhysAttack.Visibility = Visibility.Hidden;
            btnMagAttack.Visibility = Visibility.Hidden;
            btnSelfHeal.Visibility = Visibility.Hidden;
            listChat.Visibility = Visibility.Hidden;
            listChat.Items.Clear();
            chat.Visibility = Visibility.Hidden;
            tbWhoAttack.Visibility = Visibility.Hidden;
            tbEvasion.Text = string.Empty;
            tbEvasion2.Text = string.Empty;
            tbCriticalHit.Text = string.Empty;
            tbCriticalHit2.Text = string.Empty;
            tbManaPool.Text = string.Empty;
            tbManaPool2.Text = string.Empty;
        }

        private bool MannaEst()
        {
            if(cnt == -1)
            {
                if(((double.Parse(tbManaPool.Text) - 5) != 0))
                {
                    tbManaPool.Text = $"{double.Parse(tbManaPool.Text) - 5}";
                    return true;
                }
            }
            if (cnt == 1)
            {
                if ((double.Parse(tbManaPool2.Text) - 5) != 0)
                {
                    tbManaPool2.Text = $"{double.Parse(tbManaPool2.Text) - 5}";
                    return true;
                }
            }
            return false;
        }

        string APPointText;
        string APPointText2;
        string BPPointText;
        string BPPointText2;

        private void ApHead_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton currentRadioButton = (RadioButton)sender;
            APPointText = currentRadioButton.Tag.ToString();
        }

        private void ApHead2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton currentRadioButton = (RadioButton)sender;
            APPointText2 = currentRadioButton.Tag.ToString();
        }

        private void BpHead2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton currentRadioButton = (RadioButton)sender;
            BPPointText2 = currentRadioButton.Tag.ToString();
        }

        private void BpHead_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton currentRadioButton = (RadioButton)sender;
            BPPointText = currentRadioButton.Tag.ToString();
        }
    }
}
