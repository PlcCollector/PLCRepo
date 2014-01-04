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
using PLCLayer;
using DBHandler;
using DataCollectors;
using DataObjects;
using System.Collections;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PLCInterface> listOfPLCs;
        Dictionary<int, List<System.Threading.Thread>> runningThreads = new Dictionary<int,List<System.Threading.Thread>>();        

        public MainWindow()
        {
           InitializeComponent();
           listOfPLCs = PLCHelper.LoadAllPLCsFromDB();
           this.UpdateListBox();
        }       
        
        private PLCInterface loadPLCByConfigId(int configId)
        {
            PLCDBHandler PLCDBHandler = new PLCDBHandler();
            return PLCHelper.LoadPLCByID(configId);           
        }

        private void UpdateListBox()
        {
            listboxPLC.Items.Clear();

            foreach (PLCInterface plcInterface in listOfPLCs)
            {                
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = plcInterface.GetPLCConfig().plcID.ToString() + "." + plcInterface.GetPLCConfig().plcName;
                listBoxItem.Tag = plcInterface.GetPLCConfig().plcID;
                listboxPLC.Items.Add(listBoxItem);   
            }
            this.Show();

            this.SetColorOfListRunningBoxElements();
        }

      
        #region Buttons

        //Start Button
        private void Button_StartOne(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)listboxPLC.SelectedItem;
            if (selectedItem == null) return;

            int plcID = (int)selectedItem.Tag;
            PLCInterface plcInterface = GetPLCInterfaceFromList(plcID);
            this.StartOneWorkerThread(plcInterface);
            UpdateListBox();
            
        }

        //Stop last Thread Button
        private void Button_StopOne(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)listboxPLC.SelectedItem;
            if (selectedItem == null) return;

            int plcID = (int)selectedItem.Tag;
            this.StopThreadsOfPLCIfRunning(plcID);
            this.UpdateListBox();

        }

        private void ButtonStartAll_click(object sender, RoutedEventArgs e)
        {
            foreach (PLCInterface plcInterface in listOfPLCs)
            {
                this.StartOneWorkerThread(plcInterface);

            }

            UpdateListBox();
        }
        
        private void ButtonStopAll_click(object sender, RoutedEventArgs e)
        {
            foreach (PLCInterface plcInterface in listOfPLCs)
            {
                int plcID = plcInterface.GetPLCConfig().plcID;
                this.StopThreadsOfPLCIfRunning(plcID);
            }

            UpdateListBox();
        }

       
        #endregion

        #region helpers

        private void SetColorOfListRunningBoxElements()
        {
            for (int i = 0; i < listboxPLC.Items.Count; i++)
            {
                ListBoxItem currentItem = (ListBoxItem)listboxPLC.Items.GetItemAt(i);
                int currentPLCID = (int)currentItem.Tag;
                if (runningThreads.ContainsKey(currentPLCID))
                {
                    currentItem.Background = Brushes.DarkSeaGreen;
                }
            }
        }

        private PLCInterface GetPLCInterfaceFromList(int plcID)
        {
            foreach (PLCInterface plcInterface in this.listOfPLCs)
            {
                if (plcInterface.GetPLCConfig().plcID == plcID)
                    return plcInterface;
            }

            throw new ArgumentException("Invalide plcID given");
        }
        
        private void StartOneWorkerThread(PLCInterface plcInterface)
        {
            DataCollector dataCollector = new DataCollector(plcInterface);
            System.Threading.Thread workerThread;
            workerThread = new System.Threading.Thread(dataCollector.run);

            this.AddElementToRunningThreadsList(plcInterface.GetPLCConfig().plcID, workerThread);
            workerThread.Start();
        }

        private void StopThreadsOfPLCIfRunning(int plcID)
        {
            if (runningThreads.ContainsKey(plcID))
            {
                List<System.Threading.Thread> threadsToStop = runningThreads[plcID];

                foreach (System.Threading.Thread currentThread in threadsToStop)
                {
                    currentThread.Abort();
                }

                runningThreads.Remove(plcID);
            }
        }

        private void AddElementToRunningThreadsList(int plcID, System.Threading.Thread thread)
        {
            if (runningThreads.ContainsKey(plcID))
            {
                runningThreads[plcID].Add(thread);
            }
            else
            {
                List<System.Threading.Thread> runningThreadsOfType = new List<System.Threading.Thread>();
                runningThreadsOfType.Add(thread);
                runningThreads.Add(plcID, runningThreadsOfType);
            }
        }


        #endregion
    }
}
