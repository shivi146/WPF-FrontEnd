using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace WpfPoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string formName = "registrationinfo";
        readonly string TextBoxes = "TextBoxes";
        public MainWindow()
        {
            InitializeComponent();
            //Getting Form Structure Data
            try
            {
                FormStructureServiceClass formStructureServiceClass = new FormStructureServiceClass();
                string jsonString = formStructureServiceClass.GetJsonFileData(formName);
                JObject jsonUserRegistrationForm = JObject.Parse(jsonString);
                JArray txtBoxArray = (JArray)jsonUserRegistrationForm[TextBoxes];
                List<TextBoxes> lstTextBoxes = txtBoxArray.ToObject<List<TextBoxes>>();
                icTodoList.ItemsSource = lstTextBoxes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Saving User registration Details
        /// </summary>
        
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder strBuilderJsonData = new StringBuilder("{ '");
            for (int i = 0; i < icTodoList.Items.Count; i++)
            {
                if (i > 0)
                {
                    strBuilderJsonData.Append(",'");
                }
                TextBoxes textBox = (TextBoxes)icTodoList.Items[i];

                strBuilderJsonData.Append(textBox.Label).Append("':'").Append(textBox.Value).Append("'");
            }
            strBuilderJsonData.Append("}");

            SaveDetailsServiceClass saveDetailsServiceClass = new SaveDetailsServiceClass();
            HttpResponseMessage httpResponseMessage = saveDetailsServiceClass.SaveUserData(strBuilderJsonData.ToString());
            if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
            {
                MessageBox.Show("User Details have been Submited!!");                
            }
        }
    }
}

