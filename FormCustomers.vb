Imports System.Data
Imports System.Data.SqlClient
Public Class FormCustomers


    Dim OrderNumber As Integer
    Dim CustomersConnection As SqlConnection
    Dim OrdersCommand As SqlCommand
    Dim OrdersAdapter As SqlDataAdapter
    Dim OrdersTable As DataTable
    Dim CustomersCommand As SqlCommand
    Dim CustomersAdapter As SqlDataAdapter
    Dim CustomersTable As DataTable
    Dim CustomersManager As CurrencyManager
    Dim ProductsCommand As SqlCommand
    Dim ProductsAdapter As SqlDataAdapter
    Dim ProductsTable As DataTable
    Dim ProductsManager As CurrencyManager
    Dim PurchasesCommand As SqlCommand
    Dim PurchasesAdapter As SqlDataAdapter
    Dim PurchasesTable As DataTable
    Dim CustomerID As Long
    Dim MyStateP As String, MyBookmarkP As Integer
    Dim MyState As String, MyBookmark As Integer
    Dim NewCustomer As Boolean = False, SavedIndex As Integer


    Private Sub Customers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'connect to sales database
        CustomersConnection = New SqlConnection("Data Source=.\SQLEXPRESS; AttachDbFilename=" + Application.StartupPath + "\SQLKWSalesDB.mdf; Integrated Security=True; Connect Timeout=30; User Instance=True")
        CustomersConnection.Open()

        'establish Orders command object
        OrdersCommand = New SqlCommand("SELECT * FROM Orders ORDER BY OrderID", CustomersConnection)

        'establish Orders data adapter/data table
        OrdersAdapter = New SqlDataAdapter()
        OrdersAdapter.SelectCommand = OrdersCommand
        OrdersTable = New DataTable()
        OrdersAdapter.Fill(OrdersTable)

        'establish Customers command object
        CustomersCommand = New SqlCommand("SELECT * FROM Customers", CustomersConnection)

        'establish Customers data adapter/data table
        CustomersAdapter = New SqlDataAdapter()
        CustomersAdapter.SelectCommand = CustomersCommand
        CustomersTable = New DataTable()
        CustomersAdapter.Fill(CustomersTable)

        'bind controls to data table
        txtFirstName.DataBindings.Add("Text", CustomersTable, "FirstName")
        txtLastName.DataBindings.Add("Text", CustomersTable, "LastName")
        txtAddress.DataBindings.Add("Text", CustomersTable, "Address")
        txtCity.DataBindings.Add("Text", CustomersTable, "City")
        txtState.DataBindings.Add("Text", CustomersTable, "State")
        txtZip.DataBindings.Add("Text", CustomersTable, "Zip")
        'TxtPhone.DataBindings.Add("Text", CustomersTable, "Phone")

        'establish currency manager
        CustomersManager = DirectCast(Me.BindingContext(CustomersTable), CurrencyManager)

        'establish Products command object
        ProductsCommand = New SqlCommand("SELECT * FROM Products ORDER BY Description", CustomersConnection)

        'establish Products data adapter/data table
        ProductsAdapter = New SqlDataAdapter()
        ProductsAdapter.SelectCommand = ProductsCommand
        ProductsTable = New DataTable()
        ProductsAdapter.Fill(ProductsTable)

        'bind combobox to data table
        cboProducts.DataSource = ProductsTable
        cboProducts.DisplayMember = "Description"
        cboProducts.ValueMember = "ProductID"

        'establish Purchases command object
        PurchasesCommand = New SqlCommand("SELECT * FROM Purchases ORDER BY OrderID", CustomersConnection)

        'establish Purchases data adapter/data table
        PurchasesAdapter = New SqlDataAdapter()
        PurchasesAdapter.SelectCommand = PurchasesCommand
        PurchasesTable = New DataTable()
        PurchasesAdapter.Fill(PurchasesTable)

        'Fill customers combo box
        Call FillCustomers()
        OrderNumber = 0
        Call NewOrder()
    End Sub
    Private Sub FillCustomers()
        Dim NRec As Integer
        cboCustomers.Items.Clear()
        If CustomersTable.Rows.Count <> 0 Then
            For NRec = 0 To CustomersTable.Rows.Count - 1
                cboCustomers.Items.Add(CustomerListing(CustomersTable.Rows(NRec).Item("LastName").ToString, CustomersTable.Rows(NRec).Item("FirstName").ToString, CustomersTable.Rows(NRec).Item("CustomerID").ToString))
            Next NRec
        End If
    End Sub
    Private Function CustomerListing(ByVal LastName As String, ByVal FirstName As String, ByVal ID As String) As String
        Return (LastName + ", " + FirstName + " (" + ID + ")")
    End Function
    Private Sub Customers_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If NewCustomer Then
            MessageBox.Show("You must finish the current edit before stopping.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.Cancel = True
        Else
            Try

                'save the tables to the database file
                Dim OrdersAdapterCommands As New SqlCommandBuilder(OrdersAdapter)
                OrdersAdapter.Update(OrdersTable)
                Dim CustomersAdapterCommands As New SqlCommandBuilder(CustomersAdapter)
                CustomersAdapter.Update(CustomersTable)
                Dim ProductsAdapterCommands As New SqlCommandBuilder(ProductsAdapter)
                ProductsAdapter.Update(ProductsTable)
                Dim PurchasesAdapterCommands As New SqlCommandBuilder(PurchasesAdapter)
                PurchasesAdapter.Update(PurchasesTable)
            Catch ex As Exception
                MessageBox.Show("Error saving database", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'close the connection
            CustomersConnection.Close()

            'dispose of the objects
            OrdersCommand.Dispose()
            OrdersAdapter.Dispose()
            OrdersTable.Dispose()
            CustomersCommand.Dispose()
            CustomersAdapter.Dispose()
            CustomersTable.Dispose()
            ProductsCommand.Dispose()
            ProductsAdapter.Dispose()
            ProductsTable.Dispose()
            PurchasesCommand.Dispose()
            PurchasesAdapter.Dispose()
            PurchasesTable.Dispose()
        End If
    End Sub

    Private Sub NewOrder()
        Dim IDString As String
        Dim ThisDay As Date = Now
        lblDate.Text = Format(ThisDay, "d")

        'Build order ID as string
        OrderNumber += 1
        IDString = Mid(ThisDay.Year.ToString, 3, 2)
        IDString += Format(ThisDay.Month, "00")
        IDString += Format(ThisDay.Day, "00")
        IDString += Format(OrderNumber, "000")
        lblOrderID.Text = IDString
        If cboCustomers.Items.Count <> 0 Then
            cboCustomers.SelectedIndex = 0
        End If

        'Clear purchase information
        cboCustomers.SelectedIndex = -1
        nudQuantity.Value = 1
        FormShoppingCart.lblTotal.Text = "0.00"
        FormShoppingCart.lstCart.Items.Clear()
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub CboCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCustomers.SelectedIndexChanged
        If NewCustomer Then Exit Sub
        Dim ID As String, PL As Integer
        Try
            PL = InStr(cboCustomers.SelectedItem.ToString, "(")
            If PL = 0 Then Exit Sub

            'extract ID from selected item
            ID = Mid(cboCustomers.SelectedItem.ToString, PL + 1, Len(cboCustomers.SelectedItem.ToString) - PL - 1)
            CustomersTable.DefaultView.Sort = "CustomerID"
            CustomersManager.Position = CustomersTable.DefaultView.Find(ID)
            CustomerID = CLng(ID)
        Catch ex As Exception
            MessageBox.Show("Could not find customer", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        'enable text boxes for editing and add row
        NewCustomer = True
        txtFirstName.ReadOnly = False
        txtLastName.ReadOnly = False
        txtAddress.ReadOnly = False
        txtCity.ReadOnly = False
        txtState.ReadOnly = False
        txtZip.ReadOnly = False
        btnNew.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        SavedIndex = cboCustomers.SelectedIndex
        cboCustomers.SelectedIndex = -1
        cboCustomers.Enabled = False
        CustomersManager.AddNew()
        txtFirstName.Focus()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btnSave.Click

        'return to previous customer
        NewCustomer = False
        txtFirstName.ReadOnly = True
        txtLastName.ReadOnly = True
        txtAddress.ReadOnly = True
        txtCity.ReadOnly = True
        txtState.ReadOnly = True
        txtZip.ReadOnly = True
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        CustomersManager.CancelCurrentEdit()
        cboCustomers.Enabled = True
        cboCustomers.SelectedIndex = SavedIndex
    End Sub

    Private Sub TxtFirstName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFirstName.KeyPress
        If e.KeyChar = ControlChars.Cr Then txtLastName.Focus()
    End Sub

    Private Sub TxtLastName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLastName.KeyPress
        If e.KeyChar = ControlChars.Cr Then txtAddress.Focus()
    End Sub

    Private Sub TxtAddress_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress.KeyPress
        If e.KeyChar = ControlChars.Cr Then txtCity.Focus()
    End Sub

    Private Sub TxtCity_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
        If e.KeyChar = ControlChars.Cr Then txtState.Focus()
    End Sub

    Private Sub TxtState_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtState.KeyPress
        If e.KeyChar = ControlChars.Cr Then txtZip.Focus()
    End Sub

    Private Sub TxtZip_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZip.KeyPress
        If e.KeyChar = ControlChars.Cr Then btnSave.Focus()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim AllOK As Boolean = True

        'make sure there are entries
        If txtFirstName.Text = "" Then AllOK = False
        If txtLastName.Text = "" Then AllOK = False
        If txtAddress.Text = "" Then AllOK = False
        If txtCity.Text = "" Then AllOK = False
        If txtState.Text = "" Then AllOK = False
        If txtZip.Text = "" Then AllOK = False
        If Not (AllOK) Then
            MessageBox.Show("All text boxes require an entry.", "Information Missing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtFirstName.Focus()
            Exit Sub
        End If
        CustomersManager.EndCurrentEdit()

        'save to database then reopen to retrieve assigned CustomerID
        Dim SavedFirstName As String = txtFirstName.Text
        Dim SavedLastName As String = txtLastName.Text
        Dim CustomersAdapterCommands As New SqlCommandBuilder(CustomersAdapter)
        CustomersAdapter.Update(CustomersTable)
        CustomersConnection.Close()

        'reconnect to sales database
        CustomersConnection = New SqlConnection("Data Source=.\SQLEXPRESS; AttachDbFilename=" + Application.StartupPath + "\SQLKWSalesDB.mdf; Integrated Security=True; Connect Timeout=30; User Instance=True")
        CustomersConnection.Open()
        CustomersCommand = New SqlCommand("SELECT * FROM Customers", CustomersConnection)
        CustomersAdapter = New SqlDataAdapter()
        CustomersAdapter.SelectCommand = CustomersCommand
        CustomersTable = New DataTable()
        CustomersAdapter.Fill(CustomersTable)

        'rebind controls to data table
        txtFirstName.DataBindings.Clear()
        txtLastName.DataBindings.Clear()
        txtLastName.DataBindings.Clear()
        txtAddress.DataBindings.Clear()
        txtCity.DataBindings.Clear()
        txtState.DataBindings.Clear()
        txtZip.DataBindings.Clear()
        txtFirstName.DataBindings.Add("Text", CustomersTable, "FirstName")
        txtLastName.DataBindings.Add("Text", CustomersTable, "LastName")
        txtAddress.DataBindings.Add("Text", CustomersTable, "Address")
        txtCity.DataBindings.Add("Text", CustomersTable, "City")
        txtState.DataBindings.Add("Text", CustomersTable, "State")
        txtZip.DataBindings.Add("Text", CustomersTable, "Zip")
        'TxtPhone.DataBindings.Add("Text", CustomersTable, "Phone")
        CustomersManager = DirectCast(Me.BindingContext(CustomersTable), CurrencyManager)

        'find added customer
        Dim I As Integer, ID As String = ""
        For I = 0 To CustomersTable.Rows.Count - 1
            If CustomersTable.Rows(I).Item("FirstName").ToString = SavedFirstName And CustomersTable.Rows(I).Item("LastName").ToString = SavedLastName Then
                ID = CustomersTable.Rows(I).Item("CustomerID").ToString
                Exit For
            End If
        Next
        cboCustomers.Enabled = True

        'refill table
        Call FillCustomers()

        'display new customer
        NewCustomer = False
        txtFirstName.ReadOnly = True
        txtLastName.ReadOnly = True
        txtAddress.ReadOnly = True
        txtCity.ReadOnly = True
        txtState.ReadOnly = True
        txtZip.ReadOnly = True
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        cboCustomers.SelectedItem = CustomerListing(SavedLastName, SavedFirstName, ID)
    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim UnitPrice As Single
        If cboCustomers.SelectedIndex = -1 Then
            MessageBox.Show("You must select a product.", "Purchase Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'Find unit price of selected product
        Dim NRec As Integer
        For NRec = 0 To ProductsTable.Rows.Count - 1
            If ProductsTable.Rows(NRec).Item("Description").ToString = cboCustomers.Text.ToString Then
                UnitPrice = CSng(ProductsTable.Rows(NRec).Item("Price"))
                Exit For
            End If
        Next
        FormShoppingCart.lstCart.Items.Add(Format(nudQuantity.Value, "##") + " " + cboCustomers.SelectedValue.ToString + "-" + cboCustomers.Text.ToString + " " + Format(UnitPrice, "$0.00"))

        'Adjust total price
        FormShoppingCart.lblTotal.Text = Format(Val(FormShoppingCart.lblTotal.Text) + nudQuantity.Value * UnitPrice, "0.00")
    End Sub

    Private Sub BtnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim Q As Integer, P As Single, I As Integer
        If FormShoppingCart.lstCart.SelectedIndex <> -1 Then

            'Adjust total before removing
            'find Q (quantity) and P (price)
            I = InStr(FormShoppingCart.lstCart.Text, " ")
            Q = CInt(Mid(FormShoppingCart.lstCart.Text, 1, I - 1))
            I = InStr(FormShoppingCart.lstCart.Text, "$")
            P = CSng(Mid(FormShoppingCart.lstCart.Text, I + 1, Len(FormShoppingCart.lstCart.Text) - I))
            FormShoppingCart.lblTotal.Text = Format(Val(FormShoppingCart.lblTotal.Text) - Q * P, "0.00")
            FormShoppingCart.lstCart.Items.RemoveAt(FormShoppingCart.lstCart.SelectedIndex)
        End If
    End Sub

    Private Sub BtnSubmitOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitOrder.Click
        Dim I As Integer, J As Integer
        Dim Q As Integer, ID As String

        'Make sure there is customer information
        If cboCustomers.SelectedIndex = -1 Then
            MessageBox.Show("You need to select a customer.", "Submit Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If FormShoppingCart.lstCart.Items.Count = 0 Then
            MessageBox.Show("You need to select some items.", "Submit Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'Submit purchases to database
        Dim NewRow As DataRow
        NewRow = OrdersTable.NewRow
        NewRow.Item("OrderID") = lblOrderID.Text
        NewRow.Item("CustomerID") = CustomerID
        NewRow.Item("OrderDate") = lblDate.Text
        OrdersTable.Rows.Add(NewRow)
        For I = 0 To FormShoppingCart.lstCart.Items.Count - 1
            NewRow = PurchasesTable.NewRow
            J = InStr(FormShoppingCart.lstCart.Items.Item(I).ToString, " ")
            Q = CInt(Mid(FormShoppingCart.lstCart.Items.Item(I).ToString, 1, J - 1))
            ID = Mid(FormShoppingCart.lstCart.Items.Item(I).ToString, J + 1, 6)
            NewRow.Item("OrderID") = lblOrderID.Text
            NewRow.Item("ProductID") = ID
            NewRow.Item("Quantity") = Q
            PurchasesTable.Rows.Add(NewRow)

            'Update number sold
            'find row with correct productid
            Dim PR As Integer
            For PR = 0 To ProductsTable.Rows.Count - 1
                If ProductsTable.Rows(PR).Item("ProductID").ToString = ID Then
                    Exit For
                End If
            Next
            ProductsTable.Rows(PR).Item("NumberSold") = CInt(ProductsTable.Rows(PR).Item("NumberSold")) + Q
        Next I
        If MessageBox.Show("Do you want a printed invoice?", "Print Inquiry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Call PrintInvoice()
        End If
        Call NewOrder()
    End Sub
    Private Sub PrintInvoice()

        'Declare the document
        Dim RecordDocument As Drawing.Printing.PrintDocument

        'Create the document and name it
        RecordDocument = New Drawing.Printing.PrintDocument()
        RecordDocument.DocumentName = "Sales Invoice"

        'Add code handler
        AddHandler RecordDocument.PrintPage, AddressOf Me.PrintInvoicePage

        'Print document
        RecordDocument.Print()

        'Dispose of document when done printing
        RecordDocument.Dispose()
    End Sub






    Private Sub PrintInvoicePage(ByVal sender As Object, ByVal e As Drawing.Printing.PrintPageEventArgs)
        Dim Y As Integer = 100
        Dim S As String
        Dim I As Integer
        Dim J As Integer
        Dim TI As String
        Dim Q As String, ID As String, Desc As String, Unit As String, T As String
        Dim MyFont As Font = New Font("Courier New", 14, FontStyle.Bold)

        'Print Heading
        e.Graphics.DrawString(" MFPizza Order " + lblOrderID.Text, MyFont, Brushes.Black, 100, Y)
        Y += CInt(MyFont.GetHeight)
        e.Graphics.DrawString("Order Date " + lblDate.Text, MyFont, Brushes.Black, 100, Y)
        Y += 2 * CInt(MyFont.GetHeight)

        'Print buyer address
        MyFont = New Font("Courier New", 12, FontStyle.Regular)
        e.Graphics.DrawString(txtFirstName.Text + " " + txtLastName.Text, MyFont, Brushes.Black, 100, Y)
        Y += CInt(MyFont.GetHeight)
        e.Graphics.DrawString(txtAddress.Text, MyFont, Brushes.Black, 100, Y)
        Y += CInt(MyFont.GetHeight)
        e.Graphics.DrawString(txtCity.Text + ", " + txtState.Text + " " + txtZip.Text, MyFont, Brushes.Black, 100, Y)
        Y += 2 * CInt(MyFont.GetHeight)

        'Print items purchased and totals
        e.Graphics.DrawString("Qty  ProductID         Description         Unit    Total", MyFont, Brushes.Black, 100, Y)
        Y += CInt(MyFont.GetHeight)
        e.Graphics.DrawString("--------------------------------------------------------", MyFont, Brushes.Black, 100, Y)
        Y += CInt(MyFont.GetHeight)

        'Parse the shopping cart listings
        For I = 0 To FormShoppingCart.lstCart.Items.Count - 1
            TI = FormShoppingCart.lstCart.Items.Item(I).ToString
            J = InStr(TI, " ")
            Q = Mid(TI, 1, J - 1)
            ID = Mid(TI, J + 1, 6)
            Desc = Mid(TI, J + 8, Len(TI) - (J + 7))
            J = InStr(Desc, "$")
            Unit = Mid(Desc, J + 1, Len(Desc) - J)
            Desc = Mid(Desc, 1, J - 2)
            If Len(Desc) > 25 Then Desc = Mid(Desc, 1, 25)
            S = Space(56)
            Mid(S, 4 - Len(Q), Len(Q)) = Q
            Mid(S, 8, 6) = ID
            Mid(S, 16, Len(Desc)) = Desc
            Mid(S, 48 - Len(Unit), Len(Unit)) = Unit
            T = Format(Val(Q) * Val(Unit), "0.00")
            Mid(S, 57 - Len(T), Len(T)) = T
            e.Graphics.DrawString(S, MyFont, Brushes.Black, 100, Y)
            Y += CInt(MyFont.GetHeight)
        Next I
        e.Graphics.DrawString("--------------------------------------------------------", MyFont, Brushes.Black, 100, Y)
        Y += CInt(MyFont.GetHeight)
        S = Space(56)
        Mid(S, 42, 5) = "Total"
        Mid(S, 57 - Len(FormShoppingCart.lblTotal.Text) - 1, Len(FormShoppingCart.lblTotal.Text) + 1) = "$" + FormShoppingCart.lblTotal.Text
        e.Graphics.DrawString(S, MyFont, Brushes.Black, 100, Y)
        e.HasMorePages = False
    End Sub

End Class
