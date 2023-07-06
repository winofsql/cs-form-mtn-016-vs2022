using System.Data;
using System.Data.Odbc;

namespace cs_form_mtn_016_vs2022
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        // *****************************
        // SQL文字列格納用
        // *****************************
        private string query = "select * from 社員マスタ";

        // *****************************
        // 接続文字列作成用
        // *****************************
        private OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();

            SetBuilderData();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("OK");
        }

        // *****************************
        // 接続文字列の準備
        // *****************************
        private void SetBuilderData()
        {
            // ドライバ文字列をセット ( 波型括弧{} は必要ありません ) 
            builder.Driver = "MySQL ODBC 8.0 Unicode Driver";

            // 接続用のパラメータを追加
            builder.Add("server", "localhost");
            builder.Add("database", "lightbox");
            builder.Add("uid", "root");
            builder.Add("pwd", "");
        }

        // *****************************
        // SELECT 文よりデータ表示
        // *****************************
        private void LoadMySQL()
        {

            // 接続と実行用のクラス
            using (OdbcConnection connection = new OdbcConnection())
            using (OdbcCommand command = new OdbcCommand())
            {
                // 接続文字列
                connection.ConnectionString = builder.ConnectionString;

                try
                {
                    // 接続文字列を使用して接続
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // コマンドオブジェクトに接続をセット
                command.Connection = connection;
                // コマンドを通常 SQL用に変更
                command.CommandType = CommandType.Text;

                // *****************************
                // 実行 SQL
                // *****************************
                command.CommandText = query;

                try
                {
                    // レコードセット取得
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        // データを格納するテーブルクラス
                        DataTable dataTable = new DataTable();

                        // DataReader よりデータを格納
                        dataTable.Load(reader);

                        // 画面の一覧表示用コントロールにセット
                        dataGridView1.DataSource = dataTable;

                        // リーダを使い終わったので閉じる
                        reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    // 接続解除
                    connection.Close();
                    MessageBox.Show(ex.Message);
                    return;
                }

                // 接続解除
                connection.Close();
            }

            // カラム幅の自動調整
            dataGridView1.AutoResizeColumns();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadMySQL();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }
            int column = e.ColumnIndex;
            string text = dataGridView1.Rows[row].Cells["社員コード"].Value.ToString();

            form1.社員コード.Text = text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int row = dataGridView1.SelectedRows[0].Index - 1;
                string text = dataGridView1.Rows[row].Cells["社員コード"].Value.ToString();

                form1.社員コード.Text = text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

            }


        }
    }
}
