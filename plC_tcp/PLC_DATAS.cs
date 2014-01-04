using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace plC_tcp
{
    class PLC_DATAS
    {
        private libnodave.daveOSserialType fds;
        private libnodave.daveInterface di;
        private libnodave.daveConnection dc;

        private int i = 0;
        private int a = 0;
        private int j = 0;
        private int res = 0;
        private int b = 0;
        private int c = 0, d= 0, e=0, f= 0, g=0;
        
        private byte[] test;
        private byte[] test2;
        private string connection;

      
        public PLC_DATAS ()
	    {
            fds = new libnodave.daveOSserialType();
            di = new libnodave.daveInterface(fds, "IF1", 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
            //dc = new libnodave.daveConnection();
	    }

        public string ConnectPLC(string IPAdress, int rack, int slot)
        {
            
            String[] timesAsString = new String[300];
            int[] results = new int[300];
            byte[][] returnedByts = new byte[300][];

            test2 = new byte[4];
            
            //if(dc.connectPLC() != 0) return "Not Connected";
            //dc.disconnectPLC();

            int offset = 0;

            
            

            for (int w = 1; w < 2; w++)
            {
                DateTime startTime = DateTime.Now;
                                
                test2 = new byte[4];
                //int[] results = this.read50(100);
                //this.write50(50);

                offset += 4;

                dc = this.GetDaveConnection(IPAdress, rack, slot);
                dc.connectPLC();

                //this.write1(125, 0 + offset, 4 + offset);
                this.write1(943, 4, 7);
                //this.write1(125, 216, 220);

                //res = dc.readBytes(libnodave.daveDB, 1, 0 + offset, 4, test2);
                //returnedByts[w - 1] = test2;
                //Array.Reverse(test2);
                //results[w-1] = BitConverter.ToInt32(test2, 0);

                //results[w] = dc.getU32At(0);

                dc.disconnectPLC();
                

                DateTime endtime = DateTime.Now;
                TimeSpan time = startTime - endtime;
                timesAsString[w] = time.ToString();
            }
            
            //dc.prepareWriteRequest();
            //dc.writeBytes(libnodave.daveFlags, 1, 0, 4, test);
           //System.Threading.Thread.Sleep(5000);
           
            //c = dc.getS32();
            //d = dc.getFloat();
            
            //res = dc.readBytes(libnodave.daveDB, 1, 0, 4, test2);
            //int result = dc.getU32At(0);
            // BitConverter.ToInt32(test2,0);
            //a = dc.getU32();

            

            //dc.disconnectPLC();
            
            return connection;
        }

       
        /// <summary>
        /// Note: after close a closePort is needed
        /// 
        /// 
        /// </summary>
        /// <param name="IPAdress"></param>
        /// <param name="rack"></param>
        /// <param name="slot"></param>
        /// <returns></returns>
        private libnodave.daveConnection GetDaveConnection(string IPAdress, int rack, int slot)
        {
            //libnodave.daveOSserialType fds;
            //libnodave.daveInterface di;
            //fds.rfd = libnodave.closeSocket(102);
            libnodave.closePort(102);
            fds.rfd = libnodave.openSocket(102, IPAdress);
            fds.wfd = fds.rfd;
            di = new libnodave.daveInterface(fds, "IF1", 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
            di.setTimeout(1000000);
            //Connect to PLC
            return new libnodave.daveConnection(di, 0, rack, slot);
        }

        //private int[] read50(int offset)
        //{
        //    int address = 0;
        //    int[] results = new int[50];

        //    for (int i = offset; i < 50; i++)
        //    {
        //        res = dc.readBytes(libnodave.daveDB, 1, offset + address, offset+4 + address, null);
        //        results[i] = dc.getU32At(0 + address);

        //        address += 4;
        //    }

        //    return results;
        //}

        //private void write50(int offset)
        //{
        //    int address = 0;
        //    int[] results = new int[50];

        //    for (int i = offset; i < offset+50; i++)
        //    {
        //        this.write1(i, offset + address, offset + 4 + address);
        //        //test = BitConverter.GetBytes(i);
        //        //Array.Reverse(test);

        //        //test2 = new byte[4];

        //        //libnodave.PDU myPDU = dc.prepareWriteRequest();
        //        //libnodave.resultSet rs = new libnodave.resultSet();
        //        //myPDU.addVarToWriteRequest(libnodave.daveDB, 1, offset + address, offset+ 4 + address, test);
        //        //dc.execWriteRequest(myPDU, rs);             

        //        address += 4;
        //    }

        //}

        private void write1(int number, int startAdress, int endAdress)
        {
            test = BitConverter.GetBytes(number);
            Array.Reverse(test);

            //test2 = new byte[4];

            libnodave.PDU myPDU = dc.prepareWriteRequest();
            libnodave.resultSet rs = new libnodave.resultSet();
            myPDU.addVarToWriteRequest(libnodave.daveDB, 2, startAdress, endAdress, test);
            dc.execWriteRequest(myPDU, rs);
        }
       
        //public int ReadBytes()
        //{
        //    dc.connectPLC();
        //    //res = dc.readBytes(libnodave.daveDB,1, 0, 16, null);
        //    //test = new byte[100];
        //    //res = dc.readBytes(libnodave.daveFlags, 1, 1, 4,test);
        //     //if (res==0)
        //     //{
           
        //    a = test[0];

        //        //a=dc.getS32();	
        //        //b=dc.getS32();
        //        //c=dc.getS32();
        //        //d=dc.getFloat();
        //     //}
        //     //else
        //     //{
        //     //    return 0;
        //     //}

        //     dc.disconnectPLC();
        //     return a;
        //}

    }
}
