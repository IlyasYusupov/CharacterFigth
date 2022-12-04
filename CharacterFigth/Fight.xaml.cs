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
                tbWhoAttack.Text = $"Атакует боец {firstCharacter.Name}";

                if((firstCharacter.Dexterity - secondCharacter.Dexterity) > 0)
                {
                    Evasion = (firstCharacter.Dexterity - secondCharacter.Dexterity) * 0.1 * 100 ;
                    tbEvasion.Text = $"{Evasion} %";
                }
                else
                {
                    Evasion = 0;
                    tbEvasion.Text = "0";
                }

                if ((secondCharacter.Dexterity - firstCharacter.Dexterity)  > 0)
                {
                    Evasion2 = (secondCharacter.Dexterity - firstCharacter.Dexterity) * 0.1 * 100;
                    tbEvasion2.Text = $"{Evasion2} %";
                }
                else
                {
                    Evasion2 = 0;
                    tbEvasion2.Text = "0";
                }

                //if ((firstCharacter.Luck - secondCharacter.Luck) * 0.5 > 0)
                //{
                //    Evasion = (firstCharacter.Dexterity - secondCharacter.Dexterity) * 0.1 * 100;
                //    tbEvasion.Text = Evasion.ToString();
                //}
                //tbCriticalHit.Text =
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
                        if (APPointText == BPPointText2)
                        {
                            if (!isBlocked())
                            {
                                if (CriticalHit)
                                {

                                }
                                else
                                {
                                    if (pbHP2.Value - firstCharacter.Strength < 0)
                                    {
                                        pbHP2.Value = 0;
                                    }
                                    pbHP2.Value = pbHP2.Value - firstCharacter.Strength;
                                    listChat.Items.Add($"- {firstCharacter.Name} нанёс урон: {firstCharacter.Strength}");
                                    HP2.Text = $"HP: {pbHP2.Value}";
                                }
                            }
                            else
                            {
                                listChat.Items.Add($"- {firstCharacter.Name} не смог пробить блок {secondCharacter.Name}");
                            }
                        }
                    }
                    else
                    {
                        listChat.Items.Add($"- {firstCharacter.Name} промахнулся по {secondCharacter.Name}");
                    }
                }
                    //    if(APPointText == BPPointText2)
                    //    {
                    //        if(isBlocked())
                    //        {
                    //            listChat.Items.Add($"- {firstCharacter.Name} не смог пробить блок {secondCharacter.Name}");
                    //        }
                    //        else
                    //        {

                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (Evasion2 != 0)
                    //        {
                    //            if(!isEvasion())
                    //            {
                    //                if (pbHP2.Value - firstCharacter.Strength < 0)
                    //                {
                    //                    pbHP2.Value = 0;
                    //                }
                    //                pbHP2.Value = pbHP2.Value - firstCharacter.Strength;
                    //                listChat.Items.Add($"- {firstCharacter.Name} нанёс урон: {firstCharacter.Strength}");
                    //                HP2.Text = $"HP: {pbHP2.Value}";
                    //            }
                    //            else
                    //            {
                    //                listChat.Items.Add($"- {firstCharacter.Name} промахнулся по {secondCharacter.Name}");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (pbHP2.Value - firstCharacter.Strength < 0)
                    //            {
                    //                pbHP2.Value = 0;
                    //            }
                    //            pbHP2.Value = pbHP2.Value - firstCharacter.Strength;
                    //            listChat.Items.Add($"- {firstCharacter.Name} нанёс урон: {secondCharacter.Strength}");
                    //            HP2.Text = $"HP: {pbHP2.Value}";
                    //        }
                    //    }
                }
                else if(cnt == 1)
                {
                    if (APPointText2 == BPPointText)
                    {
                        /////////формула пробития блока
                    }
                    else
                    {
                        if (Evasion != 0)
                        {
                            if (!isEvasion())
                            {
                                if (pbHP.Value - secondCharacter.Strength < 0)
                                {
                                    pbHP.Value = 0;
                                }
                                pbHP.Value = pbHP.Value - secondCharacter.Strength;
                                listChat.Items.Add($"- {secondCharacter.Name} нанёс урон: {pbHP.Maximum - pbHP.Value}");
                                HP.Text = $"HP: {pbHP.Value}";
                            }
                            else
                            {
                                listChat.Items.Add($"- {secondCharacter.Name} промахнулся по {firstCharacter.Name}");
                            }
                        }
                        else
                        {
                            if (pbHP.Value - secondCharacter.Strength < 0)
                            {
                                pbHP.Value = 0;
                            }
                            pbHP.Value = pbHP.Value - secondCharacter.Strength;
                            listChat.Items.Add($"- {secondCharacter.Name} нанёс урон: {pbHP.Maximum - pbHP.Value}");
                            HP.Text = $"HP: {pbHP.Value}";
                        }
                    }
                }
                cnt *= -1;
                if(cnt == 1)
                {
                    tbWhoAttack.Text = $"Атакует боец {firstCharacter.Name}";
                }
                else
                {
                    tbWhoAttack.Text = $"Атакует боец {secondCharacter.Name}";
                }
            }
            else
            {
                if(pbHP2.Value == 0 )
                {
                    MessageBox.Show($"Winner: {firstCharacter.Name} \n XP: {pbHP2.Maximum}");
                }
                else if (pbHP.Value == 0)
                {
                    MessageBox.Show($"Winner: {secondCharacter.Name} \n XP: {pbHP.Maximum}");
                }
                isFighting= false;
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
                if (prom >= 1 && prom <= (firstCharacter.Strength - secondCharacter.Strength))
                {
                    return false;
                }
                else
                    return true;
            }
            if (cnt == 1)
            {
                if (prom >= 1 && prom <= (secondCharacter.Strength - firstCharacter.Strength))
                {
                    return false;
                }
                else
                    return true;
            }
            return true;
        }

        private void btnMagAttack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelfHeal_Click(object sender, RoutedEventArgs e)
        {

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
