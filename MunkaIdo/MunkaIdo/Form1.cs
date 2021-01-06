using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MunkaIdo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new Database1Entities1())
            {
                listBox1.Items.Clear();

                foreach (var ember in context.User)
                {
                    listBox1.Items.Add(new UserItem()
                    {
                        Name = ember.Nev,
                        Id = ember.UserId
                    });
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                using (var context = new Database1Entities1())
                {
                    context.User.Add(new User()
                    {
                        Nev = textBox1.Text
                    });
                    context.SaveChanges();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var ember = (UserItem)listBox1.SelectedItem;
                using (var context = new Database1Entities1())
                {
                    User user = context.User.FirstOrDefault(a => a.UserId == ember.Id);
                    context.Time.Add(new Time()
                    {
                        User = user,

                        Date = dateTimePicker1.Value,
                        Time1 = (int)numericUpDown1.Value
                    });
                    context.SaveChanges();
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                using (var context = new Database1Entities1())
                {
                    var ember = (UserItem)listBox1.SelectedItem;
                    User user = context.User.FirstOrDefault(a => a.UserId == ember.Id);
                    listBox2.Items.Clear();

                    foreach (var ido in context.Time.Where(a => a.UserId == user.UserId))
                    {
                        listBox2.Items.Add(new TimeItem()
                        {
                            Time = ido.Time1,
                            Date = ido.Date,
                            Id = ido.Id
                        });
                    }

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var context = new Database1Entities1())
            {
                int summ = 0;
                var ember = (UserItem)listBox1.SelectedItem;
                if (listBox1.SelectedItem == null)
                    return;
                User user = context.User.FirstOrDefault(a => a.UserId == ember.Id);

                foreach (var ido in context.Time.Where(a => a.UserId == user.UserId))
                {
                    summ += ido.Time1;
                }
                textBox2.Text = summ.ToString();

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.userTableAdapter.Fill(this.database1DataSet.User);
            this.timeTableAdapter.Fill(this.database1DataSet.Time);

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
    public class UserItem
    {
        public string Name { get; set; }
        public int Id { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }

    public class TimeItem
    {
        public int Time { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }


        public override string ToString()
        {
            return Date.ToString() + " - " + Time.ToString();
        }
    }
}
