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
        readonly string textBoxes = "TextBoxes";
        /// <summary>
        /// This is the method which gets invoked on application startup.
        /// To Load UI we invoke GetFormData API and get Json response then this method parse JSON array and converts it into lstTextBoxes.
        /// This lstTextBoxes then assigned to UI.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
           
            //Getting Form Structure Data
            try
            {
                FormStructureServiceClass formStructureServiceClass = new FormStructureServiceClass();
                string jsonString = formStructureServiceClass.GetJsonFileData(formName);
                JObject jsonUserRegistrationForm = JObject.Parse(jsonString);
                JArray txtBoxArray = (JArray)jsonUserRegistrationForm[textBoxes];
                List<TextBox> lstTextBoxes = txtBoxArray.ToObject<List<TextBox>>();
                formDataList.ItemsSource = lstTextBoxes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// On click of submit button this method fetch the formDataList.
        /// This list already had Label in it but because of two way binding it also has Value that user entered in Textbox.
        /// Using this formDataList we create json request that is sent to Save user detail service.
        /// </summary>

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder strBuilderJsonData = new StringBuilder("{ '");
            for (int i = 0; i < formDataList.Items.Count; i++)
            {
                if (i > 0)
                {
                    strBuilderJsonData.Append(",'");
                }
                TextBox textBox = (TextBox)formDataList.Items[i];

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

