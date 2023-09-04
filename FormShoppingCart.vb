Public Class FormShoppingCart
    'Code finds the price and description of selected product
    'Code also adjusts the displayed total price
    Public Sub BtnRemove_Click(sender As Object, e As EventArgs)
        Dim Q As Integer, P As Single, I As Integer
        If lstCart.SelectedIndex <> -1 Then
            'Adjust total before removing
            'find Q (quantity) and P (price)
            I = InStr(lstCart.Text, " ")
            Q = CInt(Mid(lstCart.Text, 1, I - 1))
            I = InStr(lstCart.Text, "$")
            P = CSng(Mid(lstCart.Text, I + 1, Len(lstCart.Text) - I))
            lblTotal.Text = Format(Val(lblTotal.Text) - Q * P, "0.00")
            lstCart.Items.RemoveAt(lstCart.SelectedIndex)
        End If
    End Sub
End Class