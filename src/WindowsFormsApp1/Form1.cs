using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using wowerClient;

namespace WindowsFormsApp1;

public class Form1 : Form
{
	private LuaManager luamanager = new LuaManager();

	private GameLua current_gl;

	public static Dictionary<string, string> careers_sort = new Dictionary<string, string>();

	public string scriptpath = ".\\script\\";

	private KeyboardHook k_hook = new KeyboardHook();

	private KeyEventHandler myKeyEventHandeler;

	private bool is_start_sendkey;

	private Random rd = new Random();

	public Dictionary<string, DateTime> wow_time = new Dictionary<string, DateTime>();

	public string _notice;

	public Config config = new Config();

	private IContainer components;

	private ComboBox comboBox1;

	private ListView listView1;

	private ColumnHeader columnHeader1;

	private ColumnHeader columnHeader2;

	private Label label1;

	private TextBox tb_wowDir;

	private Button button1;

	private Label label2;

	private Button btn_select;

	private Button btn_start;

	private Label label3;

	private Label label4;

	private Label label5;

	private BackgroundWorker bgw_autoKey;

	private ColumnHeader columnHeader3;

	private Label label6;

	public Form1()
	{
		InitializeComponent();
	}

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr FindWindow(string string_0, string string_1);

	private void AutoKet_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		LibTest obj = (LibTest)e.UserState;
		_ = obj.string_0;
		string[] keyString = obj.keyString;
		string text = "";
		string[] array = keyString;
		foreach (string text2 in array)
		{
			text = text + text2 + ",";
		}
	}

	private void AutoKey_Complete(object sender, RunWorkerCompletedEventArgs e)
	{
		btn_start.Text = "启动";
		btn_select.Enabled = true;
		bgw_autoKey.CancelAsync();
		label5.Text = "程序已经停止！";
	}

	private void AutoKey_DoWork(object sender, DoWorkEventArgs e)
	{
		_ = DateTime.Now;
		DateTime dateTime = DateTime.Now.AddSeconds(2.0);
		GameLua gameLua = (GameLua)e.Argument;
		LibKey libKey = new LibKey();
		LibWindow libWindow = new LibWindow();
		libWindow.setWindow("魔兽世界");
		while (!bgw_autoKey.CancellationPending)
		{
			if (gameLua.string_0 == "qs")
			{
				Thread.Sleep(rd.Next(50, 70));
			}
			else
			{
				Thread.Sleep(rd.Next(80, 140));
			}
			if (libWindow.handler.ToInt32() == 0)
			{
				Thread.Sleep(1000);
			}
			if (DateTime.Now > dateTime)
			{
				if (!libWindow.checkWindowAlive(libWindow.handler, "魔兽世界"))
				{
					libWindow.setWindow("魔兽世界");
				}
				dateTime = dateTime.AddSeconds(2.0);
			}
			if (!is_start_sendkey)
			{
				continue;
			}
			//if (DateTime.Now.CompareTo(wow_time[comboBox1.Text]) >= 0)
			//{
			//	Application.Exit();
			//}
			string rGB = libWindow.getRGB();
			if (!gameLua.keys.ContainsKey(rGB))
			{
				continue;
			}
			string[] array = gameLua.keys[rGB].Skip(0).Take(gameLua.keys[rGB].Length - 1).ToArray();
			string[] array2 = array;
			foreach (string text in array2)
			{
				LibTest libTest = new LibTest();
				libTest.string_0 = rGB;
				libTest.keyString = array;
				bgw_autoKey.ReportProgress(1, libTest);
				if (text == "suspend")
				{
					continue;
				}
				if (text == "stop")
				{
					e.Result = 0;
					continue;
				}
				List<byte> list = new List<byte>();
				string[] array3 = text.Split(',');
				foreach (string key in array3)
				{
					list.Add(libKey.keys_byte[key]);
				}
				libWindow.sendKey(list);
			}
		}
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		config.read();
		config.bool_0 = true;
		tb_wowDir.Text = config.string_0;
		bgw_autoKey.DoWork += AutoKey_DoWork;
		bgw_autoKey.ProgressChanged += AutoKet_ProgressChanged;
		bgw_autoKey.WorkerSupportsCancellation = true;
		bgw_autoKey.WorkerReportsProgress = true;
		bgw_autoKey.RunWorkerCompleted += AutoKey_Complete;
		careers_sort.Add("恶魔猎手", "dh");
		careers_sort.Add("死亡骑士", "dk");
		careers_sort.Add("德鲁伊", "dly");
		careers_sort.Add("盗贼", "dz");
		careers_sort.Add("法师", "fs");
		careers_sort.Add("猎人", "lr");
		careers_sort.Add("牧师", "ms");
		careers_sort.Add("圣骑士", "qs");
		careers_sort.Add("萨满", "sm");
		careers_sort.Add("术士", "ss");
		careers_sort.Add("战士", "zs");
		careers_sort.Add("武僧", "ws");
		string[] files = Directory.GetFiles(scriptpath);
		foreach (string path in files)
		{
			Encoding encoding = Encoding.GetEncoding("utf-8");
			string[] array = File.ReadAllText(path, encoding).Split(new string[1] { "!!" }, StringSplitOptions.None);
			string[] array2 = Encoding.UTF8.GetString(Convert.FromBase64String(array[0])).Split(new string[1] { "#!!#" }, StringSplitOptions.None);
			GameLua gameLua = new GameLua();
			gameLua.string_0 = array2[0];
			gameLua.string_1 = array2[1];
			gameLua.string_3 = array2[2];
			gameLua.string_4 = array2[3];
			gameLua.string_2 = array2[4];
			if (!luamanager.GameLuas.ContainsKey(gameLua.string_3))
			{
				luamanager.GameLuas.Add(gameLua.string_3, gameLua);
			}
		}
		comboBox1.SelectedIndex = 0;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
		folderBrowserDialog.Description = "选择文件夹";
		if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
		{
			tb_wowDir.Text = folderBrowserDialog.SelectedPath;
			config.string_0 = tb_wowDir.Text;
			config.write();
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
        //if (DateTime.Now.CompareTo(wow_time[comboBox1.Text]) >= 0)
        //{
        //	MessageBox.Show("此职业时间不够了！");
        //}
        //else if (listView1.Items.Count > 0 && listView1.SelectedItems.Count > 0)
        //{
        //	ListViewItem listViewItem = listView1.SelectedItems[0];
        //	GameLua gameLua = luamanager.GameLuas[listViewItem.SubItems[0].Text];
        //	if (!luamanager.checkWowDir(tb_wowDir.Text, gameLua.string_1))
        //	{
        //		MessageBox.Show("路径选择错误！\r\n燃烧的远征请选择魔兽世界目录下的_classic_目录\r\n经典怀旧服请选择魔兽世界目录下的_classic_era_目录\r\n正式服请选择魔兽世界目录下的_retail_目录");
        //		return;
        //	}
        //	gameLua = luamanager.parseLuaFile(listViewItem.Text, config.string_1, config.string_2);
        //	LibCharacter.GetCharacter(tb_wowDir.Text);
        //	LibCharacter.CreateBindFile();
        //	luamanager.writeLua2WowDir(tb_wowDir.Text, gameLua);
        //	luamanager.writeWtfFile(tb_wowDir.Text);
        //	current_gl = gameLua;
        //	label3.Text = "您选择的脚本为：" + gameLua.string_3 + ",点启动按钮开始。";
        //	btn_start.Enabled = true;
        //}

        ListViewItem listViewItem = listView1.SelectedItems[0];
        GameLua gameLua = luamanager.GameLuas[listViewItem.SubItems[0].Text];
        if (!luamanager.checkWowDir(tb_wowDir.Text, gameLua.string_1))
        {
            MessageBox.Show("路径选择错误！\r\n燃烧的远征请选择魔兽世界目录下的_classic_目录\r\n经典怀旧服请选择魔兽世界目录下的_classic_era_目录\r\n正式服请选择魔兽世界目录下的_retail_目录");
            return;
        }
        gameLua = luamanager.parseLuaFile(listViewItem.Text, config.string_1, config.string_2);
        LibCharacter.GetCharacter(tb_wowDir.Text);
        LibCharacter.CreateBindFile();
        luamanager.writeLua2WowDir(tb_wowDir.Text, gameLua);
        //luamanager.writeWtfFile(tb_wowDir.Text);
        current_gl = gameLua;
        label3.Text = "您选择的脚本为：" + gameLua.string_3 + ",点启动按钮开始。";
        btn_start.Enabled = true;
    }

	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		listView1.Items.Clear();
		string itemText = comboBox1.GetItemText(comboBox1.SelectedItem);
		label6.Text = "到期时间：无限";
		string text = careers_sort[itemText];
		foreach (string key in luamanager.GameLuas.Keys)
		{
			GameLua gameLua = luamanager.GameLuas[key];
			if (gameLua.string_0 == text)
			{
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = key;
				listViewItem.SubItems.Add(gameLua.string_4);
				listViewItem.SubItems.Add(gameLua.string_2);
				listView1.Items.Add(listViewItem);
			}
		}
	}

	private void btn_start_Click(object sender, EventArgs e)
	{
		if (btn_start.Text == "启动")
		{
			myKeyEventHandeler = hook_keyDown;
			k_hook.Event_0 += myKeyEventHandeler;
			k_hook.Start();
			bgw_autoKey.RunWorkerAsync(current_gl);
			btn_start.Text = "停止";
			btn_select.Enabled = false;
			label5.Text = "程序已经启动！";
		}
		else if (btn_start.Text == "停止")
		{
			k_hook.Event_0 -= myKeyEventHandeler;
			myKeyEventHandeler = null;
			k_hook.Stop();
			btn_start.Text = "启动";
			btn_select.Enabled = true;
			bgw_autoKey.CancelAsync();
			label5.Text = "程序已经停止！";
		}
	}

	private void hook_keyDown(object sender, KeyEventArgs e)
	{
		string text = e.KeyValue.ToString();
		if (new string[4] { "49", "50", "51", "52" }.Contains(text))
		{
			is_start_sendkey = true;
			label5.Text = "开始！";
		}
		else if (text == "53")
		{
			label5.Text = "暂停！";
			is_start_sendkey = false;
		}
	}

	private void Form1_FormClosed(object sender, FormClosedEventArgs e)
	{
		Application.Exit();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsFormsApp1.Form1));
		this.comboBox1 = new System.Windows.Forms.ComboBox();
		this.listView1 = new System.Windows.Forms.ListView();
		this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
		this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
		this.label1 = new System.Windows.Forms.Label();
		this.tb_wowDir = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.btn_select = new System.Windows.Forms.Button();
		this.btn_start = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.bgw_autoKey = new System.ComponentModel.BackgroundWorker();
		this.label6 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox1.FormattingEnabled = true;
		this.comboBox1.Items.AddRange(new object[12]
		{
			"战士", "术士", "武僧", "萨满", "圣骑士", "牧师", "猎人", "法师", "盗贼", "德鲁伊",
			"死亡骑士", "恶魔猎手"
		});
		this.comboBox1.Location = new System.Drawing.Point(110, 12);
		this.comboBox1.Name = "comboBox1";
		this.comboBox1.Size = new System.Drawing.Size(225, 20);
		this.comboBox1.TabIndex = 0;
		this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
		this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
		this.listView1.FullRowSelect = true;
		this.listView1.HideSelection = false;
		this.listView1.Location = new System.Drawing.Point(12, 49);
		this.listView1.Name = "listView1";
		this.listView1.Size = new System.Drawing.Size(759, 421);
		this.listView1.TabIndex = 1;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.View = System.Windows.Forms.View.Details;
		this.columnHeader1.Text = "Name";
		this.columnHeader1.Width = 250;
		this.columnHeader2.Text = "Info";
		this.columnHeader2.Width = 300;
		this.columnHeader3.Text = "UpDateTime";
		this.columnHeader3.Width = 200;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 481);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(89, 12);
		this.label1.TabIndex = 2;
		this.label1.Text = "魔兽世界目录：";
		this.tb_wowDir.Location = new System.Drawing.Point(110, 476);
		this.tb_wowDir.Name = "tb_wowDir";
		this.tb_wowDir.Size = new System.Drawing.Size(447, 21);
		this.tb_wowDir.TabIndex = 3;
		this.tb_wowDir.Text = "C:\\Program Files (x86)\\World of Warcraft\\_retail_";
		this.button1.Location = new System.Drawing.Point(563, 476);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 4;
		this.button1.Text = "选择路径";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(10, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(89, 12);
		this.label2.TabIndex = 5;
		this.label2.Text = "选择您的职业：";
		this.btn_select.Location = new System.Drawing.Point(110, 522);
		this.btn_select.Name = "btn_select";
		this.btn_select.Size = new System.Drawing.Size(75, 24);
		this.btn_select.TabIndex = 6;
		this.btn_select.Text = "初始化";
		this.btn_select.UseVisualStyleBackColor = true;
		this.btn_select.Click += new System.EventHandler(button2_Click);
		this.btn_start.Location = new System.Drawing.Point(203, 522);
		this.btn_start.Name = "btn_start";
		this.btn_start.Size = new System.Drawing.Size(75, 24);
		this.btn_start.TabIndex = 7;
		this.btn_start.Text = "启动";
		this.btn_start.UseVisualStyleBackColor = true;
		this.btn_start.Click += new System.EventHandler(btn_start_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(108, 567);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(77, 12);
		this.label3.TabIndex = 8;
		this.label3.Text = "请先选择脚本";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(284, 528);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(41, 12);
		this.label4.TabIndex = 9;
		this.label4.Text = "状态：";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(324, 528);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(0, 12);
		this.label5.TabIndex = 10;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(341, 16);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 12);
		this.label6.TabIndex = 11;
		this.label6.Text = "label6";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(794, 592);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.btn_start);
		base.Controls.Add(this.btn_select);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.tb_wowDir);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.listView1);
		base.Controls.Add(this.comboBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "Form1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Form1_FormClosed);
		base.Load += new System.EventHandler(Form1_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
