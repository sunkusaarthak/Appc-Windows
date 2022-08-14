using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Boolean pstate;
        public List<Marks> Mar { get; set; }
        public static String username, Branch, Pinno, address;
        public static int pass = 10,pass1=10;
        public string mysql = "Server=freedb.tech; Port=3306; Database=freedbtech_gptpdtr; Uid=freedbtech_gpt; Pwd=Sunku@944057;";
        public string user, email, password;
        public string sql = "";
        public MySqlConnection my;
        private void label1_Click(object sender, EventArgs e)
        {
            guna2Panel3.Visible = true;
            guna2TextBox4.Select();
        }

        private void guna2GradientButton1_MouseHover(object sender, EventArgs e)
        {
            guna2GradientButton1.FillColor = Color.FromArgb(188, 134, 159);
            guna2GradientButton1.FillColor2 = Color.FromArgb(251, 98, 118);
            guna2GradientButton1.ShadowDecoration.Enabled = true;
        }

        private void guna2ControlBox2_MouseHover(object sender, EventArgs e)
        {
            guna2ControlBox2.FillColor = Color.FromArgb(107, 45, 102);
        }

        private void guna2ControlBox2_MouseLeave(object sender, EventArgs e)
        {
            guna2ControlBox2.FillColor = Color.Transparent;
        }

        private void guna2ControlBox1_MouseHover(object sender, EventArgs e)
        {
            guna2ControlBox1.FillColor = Color.FromArgb(251, 98, 118);
        }

        private void guna2ControlBox1_MouseLeave(object sender, EventArgs e)
        {
            guna2ControlBox1.FillColor = Color.Transparent;
        }

        private void guna2GradientButton1_MouseLeave(object sender, EventArgs e)
        {
            guna2GradientButton1.FillColor2 = Color.FromArgb(188, 134, 159);
            guna2GradientButton1.FillColor = Color.FromArgb(251, 98, 118);
            guna2GradientButton1.ShadowDecoration.Enabled = false;
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        private void Form1_Load(object sender, EventArgs e)
        {

            Thread t = new Thread(open);
           
            guna2TextBox1.Select();
            my = new MySqlConnection();
            my.ConnectionString = mysql;
            String r = Properties.Settings.Default.Remember;
            int de;
            Boolean b = InternetGetConnectedState(out de, 0);
            if (b)
            {
                t.Start();
                pstate = true;
                try
                {
                    my.Open();
                }
                catch (Exception e1)
                {
                    Interneterror.Visible = true;
                    guna2Panel12.Enabled = false;
                    panel5.Enabled = false;
                    label17.Enabled = false;
                    guna2Panel8.Enabled = false;
                    panel6.Enabled = false;
                    label20.Enabled = false;
                    guna2Panel1.Enabled = false;
                    guna2Panel3.Enabled = false;
                    guna2Panel4.Visible = true;
                    label3.Text = "No Internet Connection (Click Me to Retry)";
                    pstate = false;
                    Console.Write(e1.Message);
                }
            }
            else
            {
                Interneterror.Visible = true;
                guna2Panel12.Enabled = false;
                panel5.Enabled = false;
                label17.Enabled = false;
                guna2Panel8.Enabled = false;
                panel6.Enabled = false;
                label20.Enabled = false;
                guna2Panel1.Enabled = false;
                guna2Panel3.Enabled = false;
                guna2Panel4.Visible = true;
                label3.Text = "No Internet Connection (Click Me to Retry)";
                pstate = false;
                //Thread.Sleep(5000);
            }
            printdoc1.PrintPage += new PrintPageEventHandler(printdoc1_PrintPage);
            if (r.Equals("yes"))
            {
                Home_load();
            }
            /*
            guna2Panel16.Visible = false;
            bunifuCircleProgress5.Animated = false;
            panel19.Visible = true;
            guna2Panel2.FillColor = Color.Transparent;
            */
            Thread.Sleep(1000);
            t.Abort();
            Thread check = new Thread(inter);
            check.Start();
        }

        public void inter()
        {
            int de;
            while (true)
            {
                Boolean b = InternetGetConnectedState(out de, 0);
                if (b)
                {
                    if (!pstate)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            //my.Open();
                            Interneterror.Visible = false;
                            guna2Panel12.Enabled = true;
                            panel5.Enabled = true;
                            label17.Enabled = true;
                            guna2Panel8.Enabled = true;
                            panel6.Enabled = true;
                            label20.Enabled = true;
                            guna2Panel1.Enabled = true;
                            guna2Panel3.Enabled = true;
                            guna2Panel4.Visible = false;
                            label3.Text = "***";
                        }));
                        pstate = true;
                    }
                }
                else
                {
                    if (pstate)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            Interneterror.Visible = true;
                            guna2Panel12.Enabled = false;
                            panel5.Enabled = false;
                            label17.Enabled = false;
                            guna2Panel8.Enabled = false;
                            panel6.Enabled = false;
                            label20.Enabled = false;
                            guna2Panel1.Enabled = false;
                            guna2Panel3.Enabled = false;
                            guna2Panel4.Visible = true;
                            label3.Text = "No Internet Connection (Click Me to Retry)";
                        }));
                        pstate = false;
                    }
                }
            }
        }

        Home h;

        public void open()
        { 
            /*splashScreen.SplashForm sp = new SplashScreen.SplashForm();
            sp.AppName = "Appc";
            sp.Font = new Font("poppins", 14);
            sp.StartPosition = FormStartPosition.CenterScreen;
            sp.BackColor = Color.White;
            sp.Icon = Properties.Resources._4_Victory_01_removebg;
            Application.Run(sp);
            */
            h = new Home();
            try
            {
                Application.Run(h);
            }catch(Exception e12)
            {
                Application.ExitThread();
                Console.Write(e12.Message);
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            guna2Panel5.Enabled = false;
            int de;
            Boolean b = InternetGetConnectedState(out de, 0);
            if(b)
            {
                //my.Open();
                Interneterror.Visible = false;
                guna2Panel12.Enabled = true;
                panel5.Enabled = true;
                label17.Enabled = true;
                guna2Panel8.Enabled = true;
                panel6.Enabled = true;
                label20.Enabled = true;
                guna2Panel1.Enabled = true;
                guna2Panel3.Enabled = true;
                guna2Panel4.Visible = false;
                label3.Text = "***";
                pstate = true;
            }
            guna2Panel5.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            guna2Panel5.Enabled = false;
            int de;
            Boolean b = InternetGetConnectedState(out de, 0);
            if (b)
            {
                //my.Open();
                Interneterror.Visible = false;
                guna2Panel12.Enabled = true;
                panel5.Enabled = true;
                label17.Enabled = true;
                guna2Panel8.Enabled = true;
                panel6.Enabled = true;
                label20.Enabled = true;
                guna2Panel1.Enabled = true;
                guna2Panel3.Enabled = true;
                guna2Panel4.Visible = false;
                label3.Text = "***";
                pstate = true;
            }
            guna2Panel5.Enabled = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click_1(object sender, EventArgs e)
        { 
            guna2Panel3.Visible = false;
            guna2TextBox1.Select();
        }

        private void guna2TextBox3_IconLeftClick(object sender, EventArgs e)
        {
            if(pass1==10)
            {
                guna2TextBox3.IconLeft = Properties.Resources.icons8_private_lock_64;
                guna2TextBox3.UseSystemPasswordChar = false;
                pass1 = 100;
            }
            else if(pass1==100)
            {
                guna2TextBox3.IconLeft = Properties.Resources.icons8_lock_64;
                guna2TextBox3.UseSystemPasswordChar = true;
                pass1 = 10;
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox4.Text=="" && guna2TextBox5.Text=="" && guna2TextBox3.Text=="")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter Text";
                return;
            }
            else if (guna2TextBox4.Text == "" && guna2TextBox5.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter UserName and Email";
                return;
            }
            else if (guna2TextBox5.Text == "" && guna2TextBox3.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter Email and Password";
                return;
            }
            else if (guna2TextBox4.Text == "" && guna2TextBox3.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter UserName and Password";
                return;
            }
            else if(guna2TextBox4.Text=="")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter UserName";
                return;
            }
            else if (guna2TextBox5.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter Email";
                return;
            }
            else if (guna2TextBox3.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter Password";
                return;
            }
            else if(guna2TextBox3.Text.Length<=7)
            {
                guna2Panel4.Visible = true;
                label3.Text = "Password Length Should Be Morethan 8";
                return;
            }
            backgroundWorker3.RunWorkerAsync();      
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread tmain1 = new Thread(exec1);
            tmain1.Start();
            guna2GradientButton2.Invoke((MethodInvoker)delegate { guna2GradientButton2.Visible = false; });
        }

        public void exec1()
        {
            guna2GradientButton2.Invoke((MethodInvoker)delegate {
                bunifuCircleProgress3.Visible = true;
                bunifuCircleProgress3.Animated = true;
                user = guna2TextBox4.Text;
                email = guna2TextBox5.Text;
                password = guna2TextBox3.Text;
            });
            MySqlDataReader data=null;
            try
            {
                MySqlCommand command = new MySqlCommand(null, my);
                command.CommandText = "INSERT INTO `c#`(`username`, `email`, `password`, `gender`) VALUES (@user,@email,@passw,0)";
                command.CommandTimeout = 60;
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@passw", password);
                command.Prepare();
                data = command.ExecuteReader();
                guna2GradientButton2.Invoke((MethodInvoker)delegate
                {
                    guna2Panel4.Visible = true;
                    guna2Panel4.FillColor = Color.Green;
                    label3.Text = "User Created";
                    panel1.Visible = false;
                    guna2Panel5.Visible = true;
                    String user123;
                    user123 = user;
                    user123 = char.ToUpper(user123[0]) + user123.Substring(1);
                    try
                    {
                        char[] user12 = user123.ToCharArray();
                        for (int i = 1; i < user12.Length; i++)
                        {
                            if (user12[i - 1] == ' ')
                            {
                                if (char.IsLower(user12[i]))
                                {
                                    user12[i] = char.ToUpper(user12[i]);
                                }
                            }
                        }
                        user123 = new String(user12);
                    }
                    catch (Exception e12)
                    {

                    }

                    username = user123;
                    Properties.Settings.Default.User = username;
                    Properties.Settings.Default.Email = email;
                    Properties.Settings.Default.Remember = "yes";
                    Properties.Settings.Default.Save();
                    data.Close();
                    Home_load();
                });
                    //Home h = new Home();
                    //h.Show();
                    //this.Hide();
            }
            catch (Exception e1)
            {
                guna2GradientButton2.Invoke((MethodInvoker)delegate
                {

                    guna2Panel4.Visible = true;
                    if (e1.Message.Contains("Duplicate entry"))
                    {
                        if (e1.Message.Contains("username") && e1.Message.Contains("PRIMARY"))
                        {
                            label3.Text = "Account Already Exist with UserName and Email";
                            data.Close();
                            return;
                        }
                        else if (e1.Message.Contains("username"))
                        {
                            label3.Text = username + " UserName Already exist";
                            data.Close();
                            return;
                        }
                        else if (e1.Message.Contains("PRIMARY"))
                        {
                            label3.Text = "Email Already Used";
                            data.Close();
                            return;
                        }
                    }
                    else
                    {
                        label3.Text = e1.Message;
                    }
                    Console.WriteLine(e1.Message);
                });
            }
            guna2GradientButton2.Invoke((MethodInvoker)delegate {
                bunifuCircleProgress3.Visible = false;
                guna2GradientButton2.Visible = true;
            });
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            guna2Panel4.Visible = false;
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            guna2Panel4.Visible = false;
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            guna2Panel4.Visible = false;
        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {
            guna2Panel4.Visible = false;
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            guna2Panel4.Visible = false;
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" && guna2TextBox2.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter UserName and Password";
                return;
            }
            else if (guna2TextBox1.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter UserName";
                return;
            }
            else if (guna2TextBox2.Text == "")
            {
                guna2Panel4.Visible = true;
                label3.Text = "Enter Password";
                return;
            }
            else if (guna2TextBox2.Text.Length <= 7)
            {
                guna2Panel4.Visible = true;
                label3.Text = "Password Length Should Be Morethan 8";
                return;
            }
            backgroundWorker4.RunWorkerAsync();
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread texecmain = new Thread(execmain);
            texecmain.Start();
            guna2GradientButton1.Invoke((MethodInvoker)delegate { guna2GradientButton1.Visible = false; } );
        }

        public void execmain()
        {
            MySqlDataReader pas = null;
            guna2GradientButton1.Invoke((MethodInvoker)delegate {
                bunifuCircleProgress4.Animated = true;
                bunifuCircleProgress4.Visible=true;
            } );
            try
            {
                string email12 = guna2TextBox1.Text, pass12 = guna2TextBox2.Text, gotpass = "";
                MySqlCommand command = new MySqlCommand(null, my);
                command.CommandText = "SELECT * FROM `c#` WHERE email=@email || username=@email";
                command.CommandTimeout = 60;
                command.Parameters.AddWithValue("@email", email12);
                command.Prepare();
                pas = command.ExecuteReader();
                guna2GradientButton1.Invoke((MethodInvoker)delegate
                {
                    if (pas.HasRows)
                    {
                        while (pas.Read())
                        {
                            gotpass = pas.GetString("password");
                            username = pas.GetString("username");
                            email = pas.GetString("email");
                        }

                        if (pass12 == gotpass)
                        {
                            panel1.Visible = false;
                            guna2Panel5.Visible = true;
                            String user123;
                            user123 = username;
                            user123 = char.ToUpper(user123[0]) + user123.Substring(1);
                            try
                            {
                                char[] user12 = user123.ToCharArray();
                                for (int i = 1; i < user12.Length; i++)
                                {
                                    if (user12[i - 1] == ' ')
                                    {
                                        if (char.IsLower(user12[i]))
                                        {
                                            user12[i] = char.ToUpper(user12[i]);
                                        }
                                    }
                                }
                                user123 = new String(user12);
                            }
                            catch (Exception e12)
                            {

                            }

                            username = user123;
                            Properties.Settings.Default.User = username;
                            Properties.Settings.Default.Email = email;
                            Properties.Settings.Default.Remember = "yes";
                            Properties.Settings.Default.Save();
                            pas.Close();
                            Home_load();
                            //Home home = new Home();
                            //home.Show();
                            //this.Hide();

                        }
                        else
                        {
                            guna2Panel4.Visible = true;
                            label3.Text = "Incorrect Password";
                            pas.Close();
                            return;
                        }
                    }
                    else
                    {
                        guna2Panel4.Visible = true;
                        label3.Text = "User not Exist";
                        pas.Close();
                        return;
                    }
                });
            }
            catch (Exception e2)
            {
                guna2GradientButton1.Invoke((MethodInvoker)delegate
                {
                    guna2Panel4.Visible = true;
                    if (e2.Message.Contains("Unable to connect to any of the specified"))
                    {
                        label3.Text = "Server Not Connected";
                    }
                    else
                    {
                        label3.Text = e2.Message;
                        Console.Write(e2.Message);
                    }
                    try
                    {
                        pas.Close();
                    }catch(Exception e122)
                    {
                        Console.Write(e122.Message);
                    }
                    return;
                });
            }
            guna2GradientButton1.Invoke((MethodInvoker)delegate { 
                bunifuCircleProgress4.Visible = false;
                guna2GradientButton1.Visible = true;
            });
        }

        private void guna2Panel6_Click(object sender, EventArgs e)
        {
            About.Visible = true;
            Home.Visible = false;
            label44.Text = Properties.Settings.Default.User;
            label41.Text = Properties.Settings.Default.User;
            label40.Text = Properties.Settings.Default.Email;
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            About.Visible = true;
            Home.Visible = false;
            label44.Text = Properties.Settings.Default.User;
            label41.Text = Properties.Settings.Default.User;
            label40.Text = Properties.Settings.Default.Email;
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            About.Visible = true;
            Home.Visible = false;
            label44.Text = Properties.Settings.Default.User;
            label41.Text = Properties.Settings.Default.User;
            label40.Text = Properties.Settings.Default.Email;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            About.Visible = false;
            Home.Visible = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelhand_Click(object sender, EventArgs e)
        {
            guna2Panel5.Visible = false;
            panel1.Visible = true;
            Properties.Settings.Default.Reset();
        }

        private void labelhand_Click(object sender, EventArgs e)
        {
            guna2Panel5.Visible = false;
            panel1.Visible = true;
            Properties.Settings.Default.Reset();
        }

        private void guna2Panel11hand_Click(object sender, EventArgs e)
        {
            guna2Panel5.Visible = false;
            panel1.Visible = true;
            Properties.Settings.Default.Reset();
        }

        private void guna2Panel12hand_Click(object sender, EventArgs e)
        {
            guna2Panel5.Visible = false;
            panel1.Visible = true;
            Properties.Settings.Default.Reset();
        }

        private void guna2Panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel12_Click(object sender, EventArgs e)
        {
            UnitTest.Visible = true;
            Home.Visible = false;
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            UnitTest.Visible = true;
            Home.Visible = false;
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            UnitTest.Visible = true;
            Home.Visible = false;
        }
        string pinno1;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if((guna2ComboBox1.Text == "Semester" || guna2ComboBox1.SelectedText == "Semester") && (guna2ComboBox2.Text == "Mid" || guna2ComboBox2.SelectedText == "Mid") && guna2TextBox7.Text == "")
            {
                guna2Panel15.Visible = true;
                label18.Text = "Enter All";
                return;
            }
            else if ((guna2ComboBox1.Text == "Semester" || guna2ComboBox1.SelectedText == "Semester") && (guna2ComboBox2.Text == "Mid" || guna2ComboBox2.SelectedText == "Mid"))
            {
                guna2Panel15.Visible = true;
                label18.Text = "Select Semester and Mid";
                return;
            }
            else if ((guna2ComboBox2.Text == "Mid" || guna2ComboBox2.SelectedText == "Mid") && guna2TextBox7.Text == "")
            {
                guna2Panel15.Visible = true;
                label18.Text = "Select Mid and Pinno";
                return;
            }
            else if ((guna2ComboBox1.Text == "Semester" || guna2ComboBox1.SelectedText == "Semester") && guna2TextBox7.Text == "")
            {
                guna2Panel15.Visible = true;
                label18.Text = "Select Semester and Pinno";
                return;
            }
            else if(guna2ComboBox1.Text == "Semester" || guna2ComboBox1.SelectedText == "Semester")
            {
                guna2Panel15.Visible = true;
                label18.Text = "Semester Should Be Selected";
                return;
            }
            else if(guna2ComboBox2.Text == "Mid" || guna2ComboBox2.SelectedText == "Mid")
            {
                guna2Panel15.Visible = true;
                label18.Text = "Mid Should Be Selected";
                return;
            }
            else if(guna2NumericUpDown1.Value==0)
            {
                guna2Panel15.Visible = true;
                label18.Text = "Enter College Code";
                return;
            }
            else if (guna2TextBox7.Text=="")
            {
                guna2Panel15.Visible = true;
                label18.Text = "Enter Pinno";
                return;
            }
            else if(guna2TextBox7.Text.Length>=4)
            {
                guna2Panel15.Visible = true;
                label18.Text = "Enter Correct Pinno";
                return;
            }
            pinno1 = guna2NumericUpDown1.Value.ToString() + "-CM-" + guna2TextBox7.Text;
            if (guna2ComboBox1.SelectedIndex == 1)
		    {
                if (guna2ComboBox2.SelectedIndex == 1)
				{
					sql = "select pinno,SEM,physics,english,bce,clanguage,chy,maths,name from firstsem where pinno=@pin and SEM='1'and mid = '1'";
                }
				else if (guna2ComboBox2.SelectedText == "2nd Mid")
				{
					sql = "select pinno,SEM,physics,english,bce,clanguage,chy,maths,name from firstsem where pinno=@pin and SEM='1'and mid='2'";
                }
				else if (guna2ComboBox2.SelectedText == "3rd Mid")
				{
					sql = "select pinno,SEM,physics,english,bce,clanguage,chy,maths,name from firstsem where pinno=@pin and SEM='1'and mid='3'";
                }
            }
			else if (guna2ComboBox1.SelectedText == "3rd SEM")
			{
               if (guna2ComboBox2.SelectedText == "1st Mid")
				{
					sql = "select pinno,SEM,maths,os,deca,dbms,ds,name from thirdsem where pinno=@pin and SEM='3' and mid='1'";
                }
				else if (guna2ComboBox2.SelectedText == "2nd Mid")
				{
					sql = "select pinno,SEM,maths,os,deca,dbms,ds,name from thirdsem where pinno=@pin and SEM='3' and mid='2'";
                }
            }
			else if (guna2ComboBox1.SelectedText == "4th SEM")
			{
               if (guna2ComboBox2.SelectedText == "1st Mid")
				{
					sql = "select pinno,SEM,name,WD from fourthsem where pinno=@pin and SEM='4' and MID='1'";
                }
				else if (guna2ComboBox2.SelectedText == "2nd Mid")
				{
					sql = "select pinno,SEM,name,WD from fourthsem where pinno=@pin and SEM='4' and MID='2'";
                }
            }
			else if (guna2ComboBox1.SelectedText == "6th SEM")
			{
               if (guna2ComboBox2.SelectedText == "1st Mid")
				{
					sql = "select pinno,SEM,name,DOTNET from sixthsem where pinno=@pin and SEM='6' and MID='1'";
                }
				else if (guna2ComboBox2.SelectedText == "2nd Mid")
				{
					sql = "select pinno,SEM,name,DOTNET from sixthsem where pinno=@pin and SEM='6' and MID='2'";
                }
            }
            backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread t = new Thread(exec);
            t.Start();
            guna2Button1.Invoke((MethodInvoker)delegate { guna2Button1.Visible = false; });
        }

        public void exec()
        {
            MySqlDataReader data = null;
            guna2Button1.Invoke((MethodInvoker)delegate { bunifuCircleProgress2.Visible = true; });
            var list = new List<Marks>();
            try
            {
                MySqlCommand command = new MySqlCommand(null, my);
                command.CommandText = sql;
                command.CommandTimeout = 60;
                command.Parameters.AddWithValue("@pin", pinno1);
                command.Prepare();
                //MySqlCommand command = new MySqlCommand("select pinno,SEM,physics,english,bce,clanguage,chy,maths from firstsem where pinno='18022-CM-003' and SEM='1'and mid = '1'", my);
                data = command.ExecuteReader();
                if (data.HasRows)
                {
                    guna2Button1.Invoke((MethodInvoker)delegate
                    {
                        while (data.Read())
                        {
                            guna2Button1.Invoke((MethodInvoker)delegate
                            {
                                Hello.Visible = true;
                                Hello.Text = "Name : " + data.GetString("name");
                            });
                            if (data.GetString("SEM") == "1")
                            {
                                list.Add(new Marks() { Subject = "English", MaxMarks = "20", Mark = data.GetString("english") });
                                list.Add(new Marks() { Subject = "Physics", MaxMarks = "20", Mark = data.GetString("physics") });
                                list.Add(new Marks() { Subject = "Chemistry", MaxMarks = "20", Mark = data.GetString("chy") });
                                list.Add(new Marks() { Subject = "Maths", MaxMarks = "20", Mark = data.GetString("maths") });
                                list.Add(new Marks() { Subject = "BCE", MaxMarks = "20", Mark = data.GetString("bce") });
                                list.Add(new Marks() { Subject = "C Language", MaxMarks = "20", Mark = data.GetString("clanguage") });
                                list.Add(new Marks() { Subject = "Total", MaxMarks = "120", Mark = (Convert.ToInt32(data.GetString("english")) + Convert.ToInt32(data.GetString("physics")) + Convert.ToInt32(data.GetString("chy")) + Convert.ToInt32(data.GetString("maths")) + Convert.ToInt32(data.GetString("bce")) + Convert.ToInt32(data.GetString("clanguage")) ).ToString() });
                                guna2DataGridView1.DataSource = list;
                                guna2DataGridView1.Visible = true;
                            }
                            else if (data.GetString("SEM") == "3")
                            {
                                list.Add(new Marks() { Subject = "Maths", MaxMarks = "20", Mark = data.GetString("maths") });
                                list.Add(new Marks() { Subject = "Data Structues", MaxMarks = "20", Mark = data.GetString("ds") });
                                list.Add(new Marks() { Subject = "Operating System", MaxMarks = "20", Mark = data.GetString("os") });
                                list.Add(new Marks() { Subject = "DECA", MaxMarks = "20", Mark = data.GetString("deca") });
                                list.Add(new Marks() { Subject = "DBMS", MaxMarks = "20", Mark = data.GetString("dbms") });
                                list.Add(new Marks() { Subject = "Total", MaxMarks = "100", Mark = (Convert.ToInt32(data.GetString("maths")) + Convert.ToInt32(data.GetString("ds")) + Convert.ToInt32(data.GetString("os")) + Convert.ToInt32(data.GetString("deca")) + Convert.ToInt32(data.GetString("dbms")) ).ToString() });
                                guna2DataGridView1.DataSource = list;
                                guna2DataGridView1.Visible = true;
                            }
                            else if (data.GetString("SEM") == "4")
                            {
                                list.Add(new Marks() { Subject = "Web Designing", MaxMarks = "20", Mark = data.GetString("WD") });
                                list.Add(new Marks() { Subject = "Total", MaxMarks = "100", Mark = (Convert.ToInt32(data.GetString("WD")).ToString() )});
                                guna2DataGridView1.DataSource = list;
                                guna2DataGridView1.Visible = true;
                            }
                            else if (data.GetString("SEM") == "6")
                            {
                                list.Add(new Marks() { Subject = ".Net", MaxMarks = "20", Mark = data.GetString("DOTNET") });
                                list.Add(new Marks() { Subject = "Total", MaxMarks = "100", Mark = (Convert.ToInt32(data.GetString("DOTNET")).ToString()) });
                                guna2DataGridView1.DataSource = list;
                                guna2DataGridView1.Visible = true;
                            }
                        }
                    });
                }
                else
                {
                    guna2Button1.Invoke((MethodInvoker)delegate {
                        guna2Panel15.Visible = true;
                        label18.Text = "Incorrect Pinno";
                    });
                }
                data.Close();
            }
            catch (Exception e12)
            {
                guna2Button1.Invoke((MethodInvoker)delegate {
                    guna2Panel15.Visible = true;
                    label18.Text = e12.Message;
                });
            }
            data.Close();
            guna2Button1.Invoke((MethodInvoker)delegate {
                bunifuCircleProgress2.Visible = false;
                guna2Button1.Visible = true;
            });
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2Panel15.Visible = false;
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2Panel15.Visible = false;
        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            guna2Panel15.Visible = false;
            if(guna2NumericUpDown1.Value<=99999)
            {
                guna2NumericUpDown1.Value = 18022;
            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            guna2Panel15.Visible = false;
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            UnitTest.Visible = false;
            Home.Visible = true;
        }

        private void guna2Panel8_Click(object sender, EventArgs e)
        {
            Home.Visible = false;
            guna2Panel13.Visible = true;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            String pinno = bunifuTextBox1.Text;
            Boolean re = Regex.IsMatch(pinno, @"^[0-9]{5}[-]{1}[A-Z]{1,2}[-]{1}[0-9]{3}$");
            if (re)
            {
                if (guna2ComboBox3.SelectedIndex == 0)
                {
                    error.Visible = true;
                    errorlabel.Text = "Select Year";
                    return;
                }
                if (bunifuTextBox2.Text == "")
                {
                    error.Visible = true;
                    errorlabel.Text = "Enter Purpose";
                    return;
                }
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                if (bunifuTextBox1.Text == "")
                {
                    error.Visible = true;
                    errorlabel.Text = "Enter Pinno";
                }
                else
                {
                    error.Visible = true;
                    errorlabel.Text = "Wrong Pinno";
                }
            }
        }
        public void run()
        {
            guna2Button3.Invoke((MethodInvoker)delegate {
                bunifuCircleProgress1.Visible=true;
                bunifuCircleProgress1.Animated = true;
            });
            Application.DoEvents();
            MySqlDataReader data = null;
            try
            {
                MySqlCommand command = new MySqlCommand(null, my);
                command.CommandText = "select * from studycertificate where Pinno=@pin";
                command.CommandTimeout = 60;
                command.Parameters.AddWithValue("@pin", bunifuTextBox1.Text);
                command.Prepare();
                //MySqlCommand command = new MySqlCommand("select pinno,SEM,physics,english,bce,clanguage,chy,maths from firstsem where pinno='18022-CM-003' and SEM='1'and mid = '1'", my);
                data = command.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    int lastYear = Convert.ToInt32(data["Years"]);
                    guna2Button3.Invoke((MethodInvoker)delegate
                    {
                        panel9.Visible = true;
                        panel17.Visible = true;
                        guna2Panel13.Visible = false;
                        guna2Button5.Visible = true;
                        guna2Button6.Visible = true;
                        label37.Text = "Pinno : " + data["Pinno"].ToString();
                        label34.Text = "Branch , Year : " + data["Branch"].ToString() + " , " + guna2ComboBox3.SelectedItem;
                        label36.Text = "Shift :       " + data["Shift"].ToString();
                        label26.Text = data["Name"].ToString();
                        label25.Text = data["Father"].ToString();
                        label24.Text = lastYear.ToString() + " - " + (lastYear + 3);
                        label23.Text = bunifuTextBox2.Text;
                        Study.Visible = true;
                    });
                }
                else
                {
                    guna2Button3.Invoke((MethodInvoker)delegate
                    {
                        error.Visible = true;
                        errorlabel.Text = "Wrong Pinno";
                    });

                }
                data.Close();
            }
            catch (Exception e2)
            {
                guna2Button3.Invoke((MethodInvoker)delegate {
                    error.Visible = true;
                    errorlabel.Text = e2.Message;
                    data.Close();
                    guna2Button3.Visible = true; 
                });
            }
            guna2Button3.Invoke((MethodInvoker)delegate {
                bunifuCircleProgress1.Visible = false;
                guna2Button3.Visible = true;
            });

        }
        Bitmap MemoryImage;
        PrintDocument printdoc1 = new PrintDocument();
        PrintPreviewDialog previewdlg = new PrintPreviewDialog();
        Panel pannel = null;

        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (MemoryImage != null)
            {
                e.Graphics.DrawImage(MemoryImage, 0, 0);
                base.OnPaint(e);
            }
        }
        void printdoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (pagearea.Width / 2) - (this.panel9.Width / 2), this.panel9.Location.Y);
        }
        public void Print(Panel pnl)
        {
            pannel = pnl;
            GetPrintArea(pnl);
            previewdlg.Document = printdoc1;
            previewdlg.ShowDialog();
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Print(this.panel9);
        }

        private void bunifuTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            error.Visible = false;
        }

        private void guna2ComboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            error.Visible = false;
        }

        private void bunifuTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            error.Visible = false;
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Home.Visible = false;
            guna2Panel13.Visible = true;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2Panel13.Visible = false;
            panel9.Visible = false;
            guna2Panel13.Visible = true;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            Study.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Home.Visible = true;
            guna2Panel13.Visible = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread t = new Thread(run);
            t.Start();
            guna2Button3.Invoke((MethodInvoker)delegate { guna2Button3.Visible = false; });
        }

        private void bunifuCircleProgress1_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuCircleProgress.ProgressChangedEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel9_Click(object sender, EventArgs e)
        {
            guna2Panel16.Visible = true;
            About.Visible = false;
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            guna2Panel16.Visible = true;
            About.Visible = false;
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            guna2Panel16.Visible = true;
            About.Visible = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {
           About.Visible = false;
           Home.Visible = true;
        }

        private void label45_Click(object sender, EventArgs e)
        {
            guna2Panel16.Visible = false;
            About.Visible = true;
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            Home.Visible = false;
            guna2Panel13.Visible = true;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_IconLeftClick(object sender, EventArgs e)
        {
            if(pass==10)
            {
                guna2TextBox2.IconLeft = Properties.Resources.icons8_private_lock_64;
                guna2TextBox2.UseSystemPasswordChar = false;
                pass = 100;
            }
            else if(pass==100)
            {
                guna2TextBox2.IconLeft = Properties.Resources.icons8_lock_64;
                guna2TextBox2.UseSystemPasswordChar = true;
                pass = 10;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        public void Home_load()
        {
            panel1.Visible = false;
            guna2Panel5.Visible = true;
            label4.Text = Properties.Settings.Default.User;
            label13.Text = Properties.Settings.Default.User;
        }
    }
}
