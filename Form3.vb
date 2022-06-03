Public Class Form3
    Dim searchKey, sqlCond As String
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtName.Enabled = False
        cbCat2.Enabled = False

        btnUpdate.Enabled = False
        btnDelete.Enabled = False
        btnClear.Enabled = False
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        On Error Resume Next
        con.Open()
        cmd.CommandText = "UPDATE tbl_item SET item_name = '" & UCase(txtItem.Text) & " ', " _
        & "category = '" & UCase(cbCat.Text) & " ', " _
        & "quantity = '" & UCase(txtQty.Text) & " ', " _
        & "location = '" & UCase(txtLoc.Text) & " ' " _
        & "WHERE ID=" & lblID.Text
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        con.Close()
        UpdateSub()

        MessageBox.Show("Information Update", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub UpdateSub()
        On Error Resume Next
        ds.Clear()

        cmd = con.CreateCommand
        cmd.CommandText = "select * from tbl_item"

        da.SelectCommand = cmd

        da.Fill(ds, "tbl_item")
        lblID.DataBindings.Add("Text", ds.Tables("tbl_item"), "ID")
        txtItem.DataBindings.Add("Text", ds.Tables("tbl_item"), "item_name")
        cbCat.DataBindings.Add("Text", ds.Tables("tbl_item"), "category")
        txtQty.DataBindings.Add("Text", ds.Tables("tbl_item"), "quantity")
        txtLoc.DataBindings.Add("Text", ds.Tables("tbl_item"), "location")
        DataGridView1.DataSource = ds.Tables(0)

    End Sub
    Private Sub SearchSelection()
        If rbName.Checked = True Then
            searchKey = UCase(txtName.Text)
            sqlCond = "item_name"

        Else rbCat.Checked = True
            searchKey = UCase(cbCat2.SelectedItem)
            sqlCond = "category"
        End If
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        txtName.Enabled = True
        cbCat2.Enabled = False
    End Sub

    Private Sub rbCat_CheckedChanged(sender As Object, e As EventArgs) Handles rbCat.CheckedChanged
        txtName.Enabled = False
        cbCat2.Enabled = True
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If (MessageBox.Show("Are You want to delete this record?", "DeleteRecord",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes) Then
            con.Open()
            Dim Del As OleDb.OleDbCommand = New OleDb.OleDbCommand()
            Del.CommandText = "Delete From tbl_item Where ID = " & lblID.Text

            Del.Connection = con
            Del.ExecuteNonQuery()

            Me.BindingContext(ds.Tables("tbl_item")).RemoveAt _
                (Me.BindingContext(ds.Tables("tbl_item")).Position)
            con.Close()
            MessageBox.Show("Data Have Been Deleted", "Delete", MessageBoxButtons.OK,
                             MessageBoxIcon.Information)
        Else
            Me.Refresh()

        End If
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchSelection()
        On Error Resume Next
        ds.Clear()

        cmd = con.CreateCommand
        cmd.CommandText = "Select * from tbl_item where " & sqlCond & " Like '" & searchKey & "'"

        da.SelectCommand = cmd

        da.Fill(ds, "tbl_item")
        lblID.DataBindings.Add("Text", ds.Tables("tbl_item"), "ID")
        txtItem.DataBindings.Add("Text", ds.Tables("tbl_item"), "item_name")
        cbCat.DataBindings.Add("Text", ds.Tables("tbl_item"), "category")
        txtQty.DataBindings.Add("Text", ds.Tables("tbl_item"), "quantity")
        txtLoc.DataBindings.Add("Text", ds.Tables("tbl_item"), "location")
        DataGridView1.DataSource = ds.Tables(0)

        btnUpdate.Enabled = True
        btnDelete.Enabled = True
        btnClear.Enabled = True
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lblID.Text = ""
        txtName.Clear()
        cbCat.Text = ""
        txtQty.Clear()
        txtLoc.Clear()
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        Form2.Show()
        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Form4.Show()
    End Sub


End Class