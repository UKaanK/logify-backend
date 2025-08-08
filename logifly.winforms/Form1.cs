using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace logifly.winforms
{
    public partial class Form1 : Form
    {
        private readonly string apiBaseUrl = "http://localhost:5001/api"; // Docker'daki API URL'in

        public Form1()
        {
            InitializeComponent();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnAddLog_Click(object sender, EventArgs e)
        {
            string titleD = textBox1.Text;
            string content = txtContent.Text.Trim();
            string createdByd = txtCreatedBy.Text.Trim();

            if (string.IsNullOrEmpty(titleD) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(createdByd))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            var dto = new
            {
                title = titleD,
                message = content,
                createdBy = createdByd
            };

            using (var client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(dto);
                    var contentData = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{apiBaseUrl}/Ticket", contentData);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Log başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listBoxLogs.Items.Add($"[Eklendi] {titleD} | {content} | {createdByd}");
                        txtContent.Clear();
                        txtCreatedBy.Clear();
                    }
                    else
                    {
                        MessageBox.Show($"Hata: {response.StatusCode}", "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"API'ye bağlanırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnGetByTicketId_Click(object sender, EventArgs e)
        {
            string ticketId = txtTicketId.Text.Trim();

            if (!Guid.TryParse(ticketId, out _))
            {
                MessageBox.Show("TicketId geçerli bir GUID olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/Ticket/{ticketId}");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var ticket = JsonConvert.DeserializeObject<dynamic>(json);

                        listBoxLogs.Items.Clear();
                        listBoxLogs.Items.Add($"{ticket.id} | {ticket.title} | {ticket.message} | {ticket.status} | {ticket.createdBy}");

                    }
                    else
                    {
                        MessageBox.Show($"Hata: {response.StatusCode}", "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"API'ye bağlanırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnGetAllLogs_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{apiBaseUrl}/Ticket");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var logs = JsonConvert.DeserializeObject<dynamic[]>(json);

                        listBoxLogs.Items.Clear();
                        foreach (var log in logs)
                        {
                            listBoxLogs.Items.Add($"{log.id} | {log.title} | {log.message} | {log.status} | {log.createdBy}");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Hata: {response.StatusCode}", "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"API'ye bağlanırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTicketId.Text=Guid.NewGuid().ToString();
        }
    }
}
