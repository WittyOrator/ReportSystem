using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Speeding.Util;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;


namespace Webb.Reports.ExportReports
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ReportService : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		ExportReportsManager manager=new ExportReportsManager();

		Thread mythread;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button BtnStart;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblState;
		private System.Windows.Forms.Label lblIP;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ListBox lstMsg;
		private System.Windows.Forms.TextBox txtIP;
		private System.Windows.Forms.Label TestFile;
		private System.Windows.Forms.TextBox txtTestFile;

		 Socket socket;

		public ReportService()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.lblState.Text="Please click 'Start'to begin!!! ";

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			try 
　          {
　　           socket.Close();//释放资源 

　　           mythread.Abort ( ) ;//中止线程 

　          } 
　           catch{ } 

			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.BtnStart = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtIP = new System.Windows.Forms.TextBox();
			this.lblState = new System.Windows.Forms.Label();
			this.lblIP = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lstMsg = new System.Windows.Forms.ListBox();
			this.TestFile = new System.Windows.Forms.Label();
			this.txtTestFile = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.BtnStart);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 350);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(552, 48);
			this.panel1.TabIndex = 10;
			// 
			// BtnStart
			// 
			this.BtnStart.Location = new System.Drawing.Point(92, 8);
			this.BtnStart.Name = "BtnStart";
			this.BtnStart.Size = new System.Drawing.Size(112, 32);
			this.BtnStart.TabIndex = 9;
			this.BtnStart.Text = "Start";
			this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(300, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(120, 32);
			this.button1.TabIndex = 8;
			this.button1.Text = "Exit";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtTestFile);
			this.panel2.Controls.Add(this.TestFile);
			this.panel2.Controls.Add(this.txtIP);
			this.panel2.Controls.Add(this.lblState);
			this.panel2.Controls.Add(this.lblIP);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.textBox2);
			this.panel2.Controls.Add(this.textBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(552, 184);
			this.panel2.TabIndex = 11;
			// 
			// txtIP
			// 
			this.txtIP.Location = new System.Drawing.Point(8, 80);
			this.txtIP.Name = "txtIP";
			this.txtIP.Size = new System.Drawing.Size(176, 20);
			this.txtIP.TabIndex = 17;
			this.txtIP.Text = "";
			// 
			// lblState
			// 
			this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblState.Location = new System.Drawing.Point(16, 112);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(496, 32);
			this.lblState.TabIndex = 16;
			this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblIP
			// 
			this.lblIP.Location = new System.Drawing.Point(200, 80);
			this.lblIP.Name = "lblIP";
			this.lblIP.Size = new System.Drawing.Size(312, 24);
			this.lblIP.TabIndex = 15;
			this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "TargetFile";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "StartUpFile";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(88, 48);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(144, 24);
			this.button2.TabIndex = 12;
			this.button2.Text = "OK";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(80, 24);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(424, 20);
			this.textBox2.TabIndex = 11;
			this.textBox2.Text = "";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(80, 4);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(424, 20);
			this.textBox1.TabIndex = 10;
			this.textBox1.Text = "";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.lstMsg);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 184);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(552, 166);
			this.panel3.TabIndex = 12;
			// 
			// lstMsg
			// 
			this.lstMsg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstMsg.Location = new System.Drawing.Point(0, 0);
			this.lstMsg.Name = "lstMsg";
			this.lstMsg.Size = new System.Drawing.Size(552, 160);
			this.lstMsg.TabIndex = 2;
			// 
			// TestFile
			// 
			this.TestFile.Location = new System.Drawing.Point(16, 152);
			this.TestFile.Name = "TestFile";
			this.TestFile.Size = new System.Drawing.Size(72, 24);
			this.TestFile.TabIndex = 18;
			this.TestFile.Text = "TestFile";
			// 
			// txtTestFile
			// 
			this.txtTestFile.Location = new System.Drawing.Point(88, 152);
			this.txtTestFile.Name = "txtTestFile";
			this.txtTestFile.Size = new System.Drawing.Size(448, 20);
			this.txtTestFile.TabIndex = 19;
			this.txtTestFile.Text = "";
			// 
			// ReportService
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 398);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ReportService";
			this.Text = "ReportService";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ReportService());
		}

		protected  override  void  DefWndProc(ref  System.Windows.Forms.Message  m)  
		{  
			switch(m.Msg)  
			{  
				case  WinMessageUtil.WM_COPYDATA:  
					string  str= WinMessageUtil.ReceiveMessage(ref m);

					this.lstMsg.Items.Add(str);

                    manager.ExportReport(str);

					break;
				default:  
					break;  
			}
			base.DefWndProc(ref  m);  
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Environment.Exit(-1);
			

		}

		private void button2_Click(object sender, System.EventArgs e)
		{	
			  string  str=this.textBox1.Text+" " +this.textBox2.Text;

               manager.ExportReport(str);

			   MessageBox.Show("Test Ok");
			
		}  

		public IPAddress GetServerIP()
        {

			string strDnsName=Dns.GetHostName();

			if(txtIP.Text!=string.Empty)
			{
				strDnsName=txtIP.Text;
			}

			IPHostEntry ieh=Dns.Resolve(strDnsName);

			return ieh.AddressList[0];

           
		}


		private void StartListner()
		{
		}

		private void BeginSocketListen()
		{
			IPAddress ServerIp=GetServerIP();

			IPEndPoint iep=new IPEndPoint(ServerIp,9000);

		    socket=new 	Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

			byte[] byteMessage = new byte[1024];

			int bytes;
	
			this.lblIP.Text=iep.ToString();

			socket.Bind(iep);  

			this.lblState.Text="Ready,Listening";

			while(true)
			{
				try
				{
					socket.Listen(5);

					Socket newSocket=socket.Accept();

					bytes = newSocket.Receive(byteMessage, byteMessage.Length, 0);//从客户端接受信息

					string sTime = DateTime.Now.ToShortTimeString ( ) ;

					string msg=sTime+":"+"Message from:"; 

					string Sendmsg=Encoding.ASCII.GetString(byteMessage,0,bytes).Trim(new char[]{'\0',' '});				

					msg+=newSocket.RemoteEndPoint.ToString()+" "+Sendmsg;

					this.lstMsg.Items.Add(msg);

					this.lblState.Text="Accept message,begin to export...";

					string strMsg=manager.ExportReport(Sendmsg);
					
                   	this.lblState.Text="Send Message to client...";

					newSocket.Send(Encoding.ASCII.GetBytes(strMsg));

					this.lblState.Text="Ready,Listening";
					  
				}

				catch(SocketException ex)
				{
					this.lblIP.Text+=ex.ToString();
				}
			}
		}


		private void BeginWatchFile()
		{
			string TxtFile=this.txtTestFile.Text.Trim();

			if(TxtFile==string.Empty)
			{
				MessageBox.Show("Please input the test Files!");
			}

			while(true)
			{
				if(System.IO.File.Exists(TxtFile))
				{
					try
					{
						System.IO.StreamReader sw=new System.IO.StreamReader(TxtFile);
				
						string TargetPdfFile=sw.ReadLine();

                         string StartUpFile=sw.ReadLine();						
				
						sw.Close();

						System.IO.File.Delete(TxtFile);

						string Sendmsg=StartUpFile+" "+TargetPdfFile;

						manager.ExportReport(Sendmsg);

						this.lstMsg.Items.Add(Sendmsg);


					}
					catch
					{

					}


				}
			}
		}
		private void BtnStart_Click(object sender, System.EventArgs e)
		{
			try
			{
    			mythread = new Thread(new ThreadStart(BeginSocketListen));

//				mythread = new Thread(new ThreadStart(BeginWatchFile));

				mythread.Start();
			}

			catch(System.Exception er)
			{
				MessageBox.Show(er.Message,"Excepton",MessageBoxButtons.OK,MessageBoxIcon.Stop);

			} 

		}
		
	}
}
