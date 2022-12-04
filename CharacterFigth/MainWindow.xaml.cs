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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace CharacterFigth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Character character;
        int[] parameters;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Fill(Character newCharacter)
        {
            if(newCharacter != null)
            {
                tbName.Text = newCharacter.Name;
                tbStrength.Text = newCharacter.Strength.ToString();
                tbDexterity.Text = newCharacter.Dexterity.ToString();
                tbConstitution.Text = newCharacter.Constitution.ToString();
                tbIntellect.Text = newCharacter.Intellect.ToString();
                tbLuck.Text = newCharacter.Luck.ToString();
                tbSkillPoints.Text = newCharacter.SkillPoints.ToString();
                switch (newCharacter.LVL)
                {
                    case 1:
                        pbLVL.Maximum = Character.LVLXP[2];
                        pbLVL.Value = newCharacter.XP;
                        break;
                    case 2:
                        pbLVL.Maximum = Character.LVLXP[3];
                        pbLVL.Value = newCharacter.XP;
                        break;
                    case 3:
                        pbLVL.Maximum = Character.LVLXP[4];
                        pbLVL.Value = newCharacter.XP;
                        break;
                    case 4:
                        pbLVL.Maximum = Character.LVLXP[5];
                        pbLVL.Value = newCharacter.XP;
                        break;
                    case 5:
                        pbLVL.Maximum = Character.LVLXP[6];
                        pbLVL.Value = newCharacter.XP;
                        break;
                }
                if (newCharacter.LVL == 0)
                    newCharacter.LVL = 1;

                LVL.Text = $"LVL: {newCharacter.LVL}";
                XP.Text = $"/ {newCharacter.XP} xp";
                CalcParameters();
            }
            
        }

        private void CalcParameters()
        {
            tbHealthPoint.Text = (int.Parse(tbConstitution.Text) * 5).ToString();
            tbPhysicalAttake.Text = tbStrength.Text;
            tbMagicalAttake.Text = (int.Parse(tbIntellect.Text) * 2).ToString();
            tbManaPool.Text = (int.Parse(tbIntellect.Text) * 7).ToString();
            parameters = new int[12];
            parameters[0] = int.Parse(tbStrength.Text);
            parameters[1] = int.Parse(tbDexterity.Text);
            parameters[2] = int.Parse(tbConstitution.Text);
            parameters[3] = int.Parse(tbIntellect.Text);
            parameters[4] = int.Parse(tbHealthPoint.Text);
            parameters[5] = int.Parse(tbPhysicalAttake.Text);
            parameters[6] = int.Parse(tbMagicalAttake.Text);
            parameters[7] = int.Parse(tbManaPool.Text);
            parameters[8] = int.Parse(XP.Text.Split(' ')[1]);
            parameters[9] = int.Parse(LVL.Text.Split(' ')[1]); 
            parameters[10] =  int.Parse(tbSkillPoints.Text);
            parameters[11] = int.Parse(tbLuck.Text);
        }

        private void ButtonBoost_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text != "")
            {
                if (int.Parse(tbSkillPoints.Text) <= 0)
                {
                    MessageBox.Show("Не хватает очков навыков");
                    return;
                }
                Button currentButton = (Button)sender;
                switch (currentButton.Tag)
                {
                    case "Strength":
                        if (CheckParametersLimit(tbStrength.Text, 1, "MinStrength"))
                        {
                            tbStrength.Text = tbChangeValue(tbStrength.Text, 1);
                            //param[0] = double.Parse(tbStrength.Text);
                            CalcParameters();
                        }
                        break;
                    case "Dexterity":
                        if (CheckParametersLimit(tbDexterity.Text, 1, "MinDexterity"))
                        {
                            tbDexterity.Text = tbChangeValue(tbDexterity.Text, 1);
                            //param[1] = double.Parse(tbDexterity.Text);
                            CalcParameters();
                        }
                        break;
                    case "Constitution":
                        if (CheckParametersLimit(tbConstitution.Text, 1, "MinConstitution"))
                        {
                            tbConstitution.Text = tbChangeValue(tbConstitution.Text, 1);
                            //param[2] = double.Parse(tbConstitution.Text);
                            CalcParameters();
                        }
                        break;
                    case "Intellect":
                        if (CheckParametersLimit(tbIntellect.Text, 1, "MinIntellect"))
                        {
                            tbIntellect.Text = tbChangeValue(tbIntellect.Text, 1);
                            //param[3] = double.Parse(tbIntelligence.Text);
                            CalcParameters();
                        }
                        break;
                    case "Luck":
                        if (CheckParametersLimit(tbIntellect.Text, 1, "MinLuck"))
                        {
                            tbLuck.Text = tbChangeValue(tbLuck.Text, 1);
                            //param[3] = double.Parse(tbIntelligence.Text);
                            CalcParameters();
                        }
                        break;
                }
            }
        }

        private bool CheckParametersLimit(string textBoxText, double num, string key)// проверка выхода за границы максимума и минимума характеристик
        {
            if (num < 0 && int.Parse(textBoxText) == Character.CharacterParameters[key])
            {
                MessageBox.Show("Дальше нелзя");
                return false;
            }
            return true;
        }
        private string tbChangeValue(string textBoxText, double num)
        {
            double textBoxNum = double.Parse(textBoxText);
            tbSkillPoints.Text = (int.Parse(tbSkillPoints.Text) - num).ToString();
            return (textBoxNum + (num)).ToString();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CalcParameters();
            character = new Character(tbName.Text, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], 
                                        parameters[5], parameters[6], parameters[7], parameters[8], parameters[9], parameters[10], parameters[11]);
            this.Close();
        }
    }
}