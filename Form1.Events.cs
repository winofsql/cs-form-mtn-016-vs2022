namespace cs_form_mtn_016_vs2022
{
    partial class Form1
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            this.処理区分.SelectedIndex = 0;
            this.性別.Items.Clear();
            this.性別.Items.Add(new ComboData("男性", "0"));
            this.性別.Items.Add(new ComboData("女性", "1"));
            this.性別.SelectedIndex = 0;
        }

        // *****************************************
        // 開始時のフォーカス
        // *****************************************
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.社員コード.Focus();
            this.社員コード.SelectAll();
        }

        // *****************************************
        // ( フォームの KeyPreview : True )
        // *****************************************
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    this.SelectNextControl(this.ActiveControl, false, true, true, true);

                }
                else
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                e.Handled = true;
            }
        }


        // *****************************************
        // ファンクションキ－
        // ( フォームの KeyPreview : True )
        // *****************************************
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.ヘッド部.Enabled)
            {
                if (e.KeyCode == Keys.F3)
                {
                    this.処理区分.Focus();
                    this.処理区分.DroppedDown = true;
                }

                if (e.KeyCode == Keys.F6)
                {
                    if (this.処理区分.SelectedIndex == 1 || this.処理区分.SelectedIndex == 2)
                    {
                        Form2 form2 = new Form2(this);
                        DialogResult result = form2.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            this.社員コード.Focus();
                            確認_Click(null, null);
                        }
                        form2.Dispose();

                    }
                }
            }

        }
    }
}
