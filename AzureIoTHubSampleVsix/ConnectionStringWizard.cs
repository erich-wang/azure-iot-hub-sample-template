using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;

namespace AzureIoTHubSampleVsix
{
    class ConnectionStringWizard:IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {

            var dialog=new ConnectionStringDialog();
            var dialogCompleted = dialog.ShowModal();

            if (dialogCompleted == true)
            {
                replacementsDictionary.Add("$ioTHubConnectionString$", dialog.IoTHubConnectionString.Text);
                replacementsDictionary.Add("$deviceConnectionString$", dialog.DeviceConnectionString.Text);
            }
            else
            {
                replacementsDictionary.Add("$ioTHubConnectionString$", "HostName=$iotHubUri$;SharedAccessKeyName=$accessKeyName$;SharedAccessKey=$accessKey$");
                replacementsDictionary.Add("$deviceConnectionString$", "HostName=$iotHubUri$;DeviceId=$deviceId$;SharedAccessKey=$deviceKey$");
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

    }
}

