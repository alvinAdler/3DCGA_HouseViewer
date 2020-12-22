'Representation of a point
Public Structure TPoint
    Dim x, y, z, w As Double
End Structure

'The first representation of an edge
Public Structure TLine
    Dim p1, p2 As Integer
End Structure

'Representation of a vector
Public Structure Tvector
    Dim dir_x, dir_y, dir_z As Double
End Structure

'Representation of clipping windows
Public Structure Tclip_windows
    Dim top, bottom, left, right, front, back As Integer
End Structure

'New representation of an edge. 
'The previous representation will encounter a problem in a clipping part when--
'--a vertex must be clipped to become 2 new vertices. More detail at the end of the code. 
Public Structure New_tline
    Dim point1, point2 As TPoint
End Structure

Public Class Form1
    'Global variables
    Dim btmap As New Drawing.Bitmap(800, 800)
    Dim sample_graph As Graphics = Graphics.FromImage(btmap)

    Dim Vertices(9) As TPoint
    Dim VW(9), VR(9), VS(9) As TPoint
    Dim edges(14) As TLine
    Dim clip_windows As Tclip_windows

    Public wt()() As Double = New Double(3)() {}
    Public vt()() As Double = New Double(3)() {}
    Public st()() As Double = New Double(3)() {}

    Dim VRP As TPoint
    Dim VPN, VUP As Tvector
    Dim COP As TPoint
    Dim view_windows As New Dictionary(Of String, Double)
    Dim FP, BP As Double


    '=========== PROCEDURES ===========

    'Procedure to draw line.
    Public Sub draw_line(pointX1 As Double, pointY1 As Double, pointX2 As Double, pointY2 As Double, DrawColor As Color)
        pbox_screen.Image = btmap

        Dim i, j As Double
        Dim x, y As Double
        Dim dx, dy As Integer
        Dim m, n As Double

        Try
            dx = Math.Abs(pointX2 - pointX1)
            dy = Math.Abs(pointY2 - pointY1)
            If pointX1 <> pointX2 And pointY1 <> pointY2 Then
                m = Math.Abs(dy / dx)
                n = Math.Abs(dx / dy)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        If pointX1 < pointX2 And pointY1 = pointY2 Then 'case 1
            For i = pointX1 To pointX2
                btmap.SetPixel(i, pointY1, DrawColor)
            Next

        ElseIf pointX1 > pointX2 And pointY1 = pointY2 Then 'case 2
            For i = pointX1 To pointX2 Step -1
                btmap.SetPixel(i, pointY1, DrawColor)
            Next

        ElseIf pointX1 = pointX2 And pointY1 > pointY2 Then 'case 3
            For i = pointY1 To pointY2 Step -1
                btmap.SetPixel(pointX1, i, DrawColor)
            Next

        ElseIf pointX1 = pointX2 And pointY1 < pointY2 Then 'case 4
            For i = pointY1 To pointY2
                btmap.SetPixel(pointX1, i, DrawColor)
            Next

        ElseIf dx = dy And pointX1 < pointX2 And pointY1 > pointY2 Then 'case 5
            j = pointY1
            For i = pointX1 To pointX2
                btmap.SetPixel(i, j, DrawColor)
                j -= 1
            Next

        ElseIf dx = dy And pointX1 > pointX2 And pointY1 > pointY2 Then 'case 6
            j = pointY1
            For i = pointX1 To pointX2 Step -1
                btmap.SetPixel(i, j, DrawColor)
                j -= 1
            Next
        ElseIf dx = dy And pointX1 > pointX2 And pointY1 < pointY2 Then 'case 7
            j = pointY1
            For i = pointX1 To pointX2 Step -1
                btmap.SetPixel(i, j, DrawColor)
                j += 1
            Next
        ElseIf dx = dy And pointX1 < pointX2 And pointY1 < pointY2 Then 'case 8
            j = pointY1
            For i = pointX1 To pointX2
                btmap.SetPixel(i, j, DrawColor)
                j += 1
            Next
        ElseIf dx > dy And pointX1 < pointX2 And pointY1 > pointY2 Then 'case 9
            y = pointY1
            For x = pointX1 To pointX2
                btmap.SetPixel(x, Math.Round(y), DrawColor)
                y -= m
            Next
        ElseIf dx < dy And pointX1 < pointX2 And pointY1 > pointY2 Then 'case 10
            x = pointX1
            For y = pointY1 To pointY2 Step -1
                btmap.SetPixel(Math.Round(x), y, DrawColor)
                x += n
            Next
        ElseIf dx < dy And pointX1 > pointX2 And pointY1 > pointY2 Then 'case 11
            x = pointX1
            For y = pointY1 To pointY2 Step -1
                btmap.SetPixel(Math.Round(x), y, DrawColor)
                x -= n
            Next
        ElseIf dx > dy And pointX1 > pointX2 And pointY1 > pointY2 Then 'case 12
            y = pointY1
            For x = pointX1 To pointX2 Step -1
                btmap.SetPixel(x, Math.Round(y), DrawColor)
                y -= m
            Next
        ElseIf dx > dy And pointX1 > pointX2 And pointY1 < pointY2 Then 'case 13
            y = pointY1
            For x = pointX1 To pointX2 Step -1
                btmap.SetPixel(x, Math.Round(y), DrawColor)
                y += m
            Next
        ElseIf dx < dy And pointX1 > pointX2 And pointY1 < pointY2 Then 'case 14
            x = pointX1
            For y = pointY1 To pointY2
                btmap.SetPixel(Math.Round(x), y, DrawColor)
                x -= n
            Next
        ElseIf dx < dy And pointX1 < pointX2 And pointY1 < pointY2 Then 'case 15
            x = pointX1
            For y = pointY1 To pointY2
                btmap.SetPixel(Math.Round(x), y, DrawColor)
                x += n
            Next
        ElseIf dx > dy And pointX1 < pointX2 And pointY1 < pointY2 Then 'case 16
            y = pointY1
            For x = pointX1 To pointX2
                btmap.SetPixel(x, Math.Round(y), DrawColor)
                y += m
            Next
        End If

    End Sub

    'Procedure to set point.
    Public Sub set_point(ByRef vertex As TPoint, point_x As Double, point_y As Double, point_z As Double, Optional ByVal point_w As Double = 1)
        vertex.x = point_x
        vertex.y = point_y
        vertex.z = point_z
        vertex.w = point_w
    End Sub

    'Procedure to set edge/line.
    'Used to set the components of an edge (The first representation of edge).
    Public Sub set_edge(ByRef edge As TLine, starting_point As Integer, ending_point As Integer)
        edge.p1 = starting_point
        edge.p2 = ending_point
    End Sub

    'Procedure to set the new edges.
    'Used to set the components of an edge (The second representation of edge).
    Public Sub set_new_edge(ByRef some_edge As New_tline, starting_point As TPoint, ending_point As TPoint)
        some_edge.point1 = starting_point
        some_edge.point2 = ending_point
    End Sub

    'Procedure to set vector.
    Public Sub set_vector(ByRef vector As Tvector, direction_x As Double, direction_y As Double, direction_z As Double)
        vector.dir_x = direction_x
        vector.dir_y = direction_y
        vector.dir_z = direction_z
    End Sub

    'Procedure to set row of a matrix.
    Public Sub set_matrix_row(ByRef matrix()() As Double, row_num As Integer, value0 As Double, value1 As Double, value2 As Double, value3 As Double)
        matrix(row_num) = New Double() {value0, value1, value2, value3}
    End Sub

    'Procedure to draw the boring house.
    Public Sub draw_house(ByVal final_vertices() As TPoint, ByVal final_edges() As TLine)
        Dim index As Integer
        Dim p1, p2 As TPoint
        Dim draw_last As New List(Of TLine)

        'Iterate all of the edges that exist. 
        For index = 0 To final_edges.Length - 1
            p1 = final_vertices(final_edges(index).p1)
            p2 = final_vertices(final_edges(index).p2)
            If (final_edges(index).p1 = 0 And final_edges(index).p2 = 1) Or (final_edges(index).p1 = 1 And final_edges(index).p2 = 2) Or (final_edges(index).p1 = 2 And final_edges(index).p2 = 3) Or (final_edges(index).p1 = 3 And final_edges(index).p2 = 4) Or (final_edges(index).p1 = 4 And final_edges(index).p2 = 0) Then
                'The if condition is used to determine whether an edge belong to the front--
                '--side of the house or not. If yes, put it in "draw_last" list. 
                draw_last.Add(final_edges(index))
            Else
                draw_line(CInt(p1.x), CInt(p1.y), CInt(p2.x), CInt(p2.y), Color.Black)
            End If
        Next

        'Draw the edges that belong to the front side of the house.
        For Each line As TLine In draw_last
            p1 = final_vertices(line.p1)
            p2 = final_vertices(line.p2)
            draw_line(CInt(p1.x), CInt(p1.y), CInt(p2.x), CInt(p2.y), Color.Red)
        Next
    End Sub

    'Procedure to empty the screen.
    Public Sub clear_screen()
        pbox_screen.Image = Nothing
        sample_graph.Clear(Color.White)
    End Sub

    'Procedure to grab parameters from user and assign in to the existing global variables. 
    Public Sub grab_parameters()
        set_point(VRP, CDbl(vrp_x.Text), CDbl(vrp_y.Text), CDbl(vrp_z.Text))

        view_windows.Clear()

        view_windows.Add("umin", CDbl(u_min.Text))
        view_windows.Add("vmin", CDbl(v_min.Text))
        view_windows.Add("umax", CDbl(u_max.Text))
        view_windows.Add("vmax", CDbl(v_max.Text))

        set_vector(VPN, CDbl(vpn_x.Text), CDbl(vpn_y.Text), CDbl(vpn_z.Text))
        set_vector(VUP, CDbl(vup_x.Text), CDbl(vup_y.Text), CDbl(vup_z.Text))

        set_point(COP, CDbl(cop_x.Text), CDbl(cop_y.Text), CDbl(cop_z.Text))

        FP = CDbl(front_plane.Text)
        BP = CDbl(back_plane.Text)
    End Sub

    'Procedure to set default parameters.
    Public Sub set_default_parameters()
        vrp_x.Text = "0"
        vrp_y.Text = "0"
        vrp_z.Text = "0"

        vpn_x.Text = "0"
        vpn_y.Text = "0"
        vpn_z.Text = "1"

        vup_x.Text = "0"
        vup_y.Text = "1"
        vup_z.Text = "0"

        cop_x.Text = "0"
        cop_y.Text = "0"
        cop_z.Text = "4"

        u_min.Text = "-2"
        v_min.Text = "-2"

        u_max.Text = "2"
        v_max.Text = "2"

        front_plane.Text = "2"
        back_plane.Text = "-2"
    End Sub

    'Procedure to draw the view windows
    Public Sub draw_view_windows()
        draw_line(100, 100, 300, 100, Color.Black)
        draw_line(300, 100, 300, 300, Color.Black)
        draw_line(300, 300, 100, 300, Color.Black)
        draw_line(100, 300, 100, 100, Color.Black)
    End Sub

    'Procedure to remove an element from an array
    Public Sub remove_array_element(Of T)(ByRef arr As T(), ByVal index As Integer)
        Dim uBound = arr.GetUpperBound(0)
        Dim lBound = arr.GetLowerBound(0)
        Dim arrLen = uBound - lBound

        If index < lBound OrElse index > uBound Then
            Throw New ArgumentOutOfRangeException(
        String.Format("Index must be from {0} to {1}. Current index: {2}", lBound, uBound, index))

        Else
            'create an array 1 element less than the input array
            Dim outArr(arrLen - 1) As T
            'copy the first part of the input array
            Array.Copy(arr, 0, outArr, 0, index)
            'then copy the second part of the input array
            Array.Copy(arr, index + 1, outArr, index, uBound - index)

            arr = outArr
        End If
    End Sub

    'New procedure to draw a house
    Public Sub draw_new_house(ByVal final_all As Dictionary(Of String, List(Of New_tline)))
        Dim index As Integer = 0
        'What makes this procedure different than the previous one is the type and quantity of parameters.
        'Previously, the program used the other procedure to draw house. Thus--
        'Major part of the program used that procedure. However, due to some problem in clipping,
        'The procedure must be changed. Instead of changing everything according to this procedure, 
        'the developer decided to create seperated procedure to draw a house and only implement in--
        'small parts of the program that actually requires it.
        For Each edge1 In final_all("back")
            draw_line(CInt(edge1.point1.x), CInt(edge1.point1.y), CInt(edge1.point2.x), CInt(edge1.point2.y), Color.Black)
        Next

        For Each edge2 In final_all("front")
            draw_line(CInt(edge2.point1.x), CInt(edge2.point1.y), CInt(edge2.point2.x), CInt(edge2.point2.y), Color.Red)
        Next
    End Sub

    'Procedure to clear all list boxes
    Public Sub clear_all_listbox()
        list_matrixt1t2.Items.Clear()
        list_matrixt3.Items.Clear()
        list_matrixt4.Items.Clear()
        list_matrixt5.Items.Clear()
        list_vertices.Items.Clear()
    End Sub

    '=========== END PROCEDURES ===========



    '=========== FUNCTIONS ===========

    'Function to multiply 4x4 matrix with 4x4 matrix
    Public Function matrix_mult_4x4(matrix_a()() As Double, matrix_b()() As Double)
        Dim result_matrix(3)() As Double
        Dim temp_matrix(3) As Double
        Dim current As Double = 0

        Dim row, col, item As Integer
        'First layer of for loop: used to iterate the row of a matrix. 
        For row = 0 To 3
            'Second layer of for loop: used to iterate the column of a matrix
            For col = 0 To 3
                'Third layer of for loop: used to iterate each number in associated row and column.
                For item = 0 To 3
                    current += matrix_a(row)(item) * matrix_b(item)(col)
                Next
                temp_matrix(col) = current
                current = 0
            Next
            result_matrix(row) = temp_matrix
            ReDim temp_matrix(3)
        Next

        Return result_matrix

    End Function

    'Function to multiply 1x4 matrix (a point) with 4x4 matrix (transformation matrix)
    Public Function matrix_mult_1x4(point_a As TPoint, matrix_b()() As Double)
        'Transform a point to become a 1x4 matrix to--
        '-- ease the calculation of matrix multiplication
        Dim dummy_matrix(3) As Double
        dummy_matrix(0) = point_a.x
        dummy_matrix(1) = point_a.y
        dummy_matrix(2) = point_a.z
        dummy_matrix(3) = point_a.w

        Dim result_matrix(3) As Double
        Dim point_b As TPoint
        Dim current As Double
        Dim index1, index2 As Integer

        current = 0

        For index1 = 0 To 3
            For index2 = 0 To 3
                current += dummy_matrix(index2) * matrix_b(index2)(index1)
            Next
            result_matrix(index1) = current
            current = 0
        Next

        point_b.x = result_matrix(0)
        point_b.y = result_matrix(1)
        point_b.z = result_matrix(2)
        point_b.w = result_matrix(3)

        'Transform the result back as a point.
        Return point_b

    End Function

    'Function to find a dot product between 2 vector
    Public Function find_dotProduct(vector_a As Tvector, vector_b As Tvector)
        Return (vector_a.dir_x * vector_b.dir_x) + (vector_a.dir_y * vector_b.dir_y) + (vector_a.dir_z * vector_b.dir_z)
    End Function

    'Function to find a cross product between 2 vector
    Public Function find_crossProduct(vector_a As Tvector, vector_b As Tvector)
        Dim result_vector As Tvector
        result_vector.dir_x = (vector_a.dir_y * vector_b.dir_z) - (vector_b.dir_y * vector_a.dir_z)
        result_vector.dir_y = (vector_a.dir_z * vector_b.dir_x) - (vector_b.dir_z * vector_a.dir_x)
        result_vector.dir_z = (vector_a.dir_x * vector_b.dir_y) - (vector_b.dir_x * vector_a.dir_y)

        Return result_vector
    End Function

    'Function to find the vcs axes; u, v, and N axis
    Public Function find_vcs_axes(vpn As Tvector, vup As Tvector)
        Dim up_unitV, up_prime, temp_vect, temp_vect2, temp_result As Tvector
        Dim u(3), v(3), N(3) As Double
        Dim len_vpn, len_vup, len_up_prime As Double
        Dim rest_up_dot_n As Double

        'Finding the length of VPN and VUP for normalization later on.
        len_vpn = Math.Sqrt((vpn.dir_x * vpn.dir_x) + (vpn.dir_y * vpn.dir_y) + (vpn.dir_z * vpn.dir_z))
        len_vup = Math.Sqrt((vup.dir_x * vup.dir_x) + (vup.dir_y * vup.dir_y) + (vup.dir_z * vup.dir_z))

        'Finding the N axis of VCS -> Normalize VPN
        N(0) = vpn.dir_x / len_vpn
        N(1) = vpn.dir_y / len_vpn
        N(2) = vpn.dir_z / len_vpn

        'Finding the up unit vector -> result of normalization of VUP
        up_unitV.dir_x = vup.dir_x / len_vup
        up_unitV.dir_y = vup.dir_y / len_vup
        up_unitV.dir_z = vup.dir_z / len_vup

        'Previously, N is assigned as an array. Now, assign it as a vector for the cross product. 
        temp_vect2.dir_x = N(0)
        temp_vect2.dir_y = N(1)
        temp_vect2.dir_z = N(2)

        'Find the dot product between up unit vector and N
        rest_up_dot_n = find_dotProduct(up_unitV, temp_vect2)
        'Assign it to a vector called temp_vect after multiplied with N. 
        temp_vect.dir_x = rest_up_dot_n * N(0)
        temp_vect.dir_y = rest_up_dot_n * N(1)
        temp_vect.dir_z = rest_up_dot_n * N(2)

        'Finding the value of up prime. 
        up_prime.dir_x = up_unitV.dir_x - temp_vect.dir_x
        up_prime.dir_y = up_unitV.dir_y - temp_vect.dir_y
        up_prime.dir_z = up_unitV.dir_z - temp_vect.dir_z

        'Finding the length of up prime. 
        len_up_prime = Math.Sqrt((up_prime.dir_x * up_prime.dir_x) + (up_prime.dir_y * up_prime.dir_y) + (up_prime.dir_z * up_prime.dir_z))

        'V axis is the normalization of up prime vector
        v(0) = up_prime.dir_x / len_up_prime
        v(1) = up_prime.dir_y / len_up_prime
        v(2) = up_prime.dir_z / len_up_prime

        'Previously, temp_vect is used to hold the value of dot product between up unit vector and N.
        'Now it is used to store the v-axis. 
        temp_vect.dir_x = v(0)
        temp_vect.dir_y = v(1)
        temp_vect.dir_z = v(2)

        'Finding the u axis which is the cross product of v axis and N axis. 
        temp_result = find_crossProduct(temp_vect, temp_vect2)
        u(0) = temp_result.dir_x
        u(1) = temp_result.dir_y
        u(2) = temp_result.dir_z

        'Merge all of the axis to become 1 multi-dimensional array to be returned in the section--
        'of the program. 
        Dim prepare_return()() As Double = New Double(3)() {}
        prepare_return(0) = u
        prepare_return(1) = v
        prepare_return(2) = N
        Return (prepare_return)

        'The axes are returned as multidimensional array because the developer have not figured--
        '-- out a way to return the axes as an array of vectors. 
    End Function

    'Function to find a binary representation of a number.
    Public Function find_binary_number(ByVal num As Integer)
        Return Convert.ToString(num, 2).PadLeft(6, "0"c)
    End Function

    'Function to find area code of a point.
    Public Function get_area_code(ByVal sample_point As TPoint)
        Dim area_code As Integer = 0
        If sample_point.y > clip_windows.top Then
            'Intersection happens at the top border of clipping windows. 
            area_code += 32
        ElseIf sample_point.y < clip_windows.bottom Then
            'Intersection happends at the bottom border of clipping windows. 
            area_code += 16
        End If

        If sample_point.x > clip_windows.right Then
            'Intersection happens at the right border of clipping windows.
            area_code += 8
        ElseIf sample_point.x < clip_windows.left Then
            'Intersection happens at the left border of clipping windows.
            area_code += 4
        End If

        If sample_point.z > clip_windows.front Then
            'Intersection happens at the front border of clipping windows.
            area_code += 2
        ElseIf sample_point.z < clip_windows.back Then
            'Intersection happends at the back border of clipping windows.
            area_code += 1
        End If

        'Convert the number to a binary representation and then return it.
        Return find_binary_number(area_code)
    End Function

    'Function to find a logical AND operation between 2 binary numbers
    Public Function find_logical_AND(ByVal areacode1 As String, ByVal areacode2 As String)
        Dim result As String = ""
        Dim index As Integer

        For index = 0 To 5
            If areacode1(index) = "1" And areacode2(index) = "1" Then
                result += "1"
            Else
                result += "0"
            End If
        Next

        Return result
    End Function

    Public Function find_pr1(vrp As TPoint, vpn As Tvector, vup As Tvector, cop As TPoint, v_windows As Dictionary(Of String, Double), fp As Double, bp As Double)
        Dim result_axes_vcs()() As Double = New Double(3)() {}
        Dim u_axis, v_axis, N_axis As Tvector
        Dim r_prime, minus_r As Tvector
        Dim T12()() As Double = New Double(3)() {}
        Dim T3()() As Double = New Double(3)() {}
        Dim T4()() As Double = New Double(3)() {}
        Dim T5()() As Double = New Double(3)() {}
        Dim PR2()() As Double = New Double(3)() {}

        'Finding the derived parameters; CW and DOP
        Dim CW As TPoint
        Dim DOP As Tvector
        set_point(CW, (v_windows("umin") + v_windows("umax")) / 2, (v_windows("vmin") + v_windows("vmax")) / 2, 0, 1)
        DOP.dir_x = CW.x - cop.x
        DOP.dir_y = CW.y - cop.y
        DOP.dir_z = CW.z - cop.z

        'Finding the principle axes of VCS
        result_axes_vcs = find_vcs_axes(vpn, vup)
        'Unpacking the components of result_axes_vcs array to become a vector. 
        set_vector(u_axis, result_axes_vcs(0)(0), result_axes_vcs(0)(1), result_axes_vcs(0)(2))
        set_vector(v_axis, result_axes_vcs(1)(0), result_axes_vcs(1)(1), result_axes_vcs(1)(2))
        set_vector(N_axis, result_axes_vcs(2)(0), result_axes_vcs(2)(1), result_axes_vcs(2)(2))

        'Finding the r vector -> distance between the origin of WCS according to WCS to the--
        '-- origin of VCS according to WCS. (VRP - 0, 0, 0)
        set_vector(minus_r, -vrp.x, -vrp.y, -vrp.z)

        'Finding r-prime
        r_prime.dir_x = find_dotProduct(minus_r, u_axis)
        r_prime.dir_y = find_dotProduct(minus_r, v_axis)
        r_prime.dir_z = find_dotProduct(minus_r, N_axis)

        'Setting the transformation matrix of t1 and t2 (combined)
        set_matrix_row(T12, 0, u_axis.dir_x, v_axis.dir_x, N_axis.dir_x, 0)
        set_matrix_row(T12, 1, u_axis.dir_y, v_axis.dir_y, N_axis.dir_y, 0)
        set_matrix_row(T12, 2, u_axis.dir_z, v_axis.dir_z, N_axis.dir_z, 0)
        set_matrix_row(T12, 3, r_prime.dir_x, r_prime.dir_y, r_prime.dir_z, 1)

        Dim shear_x, shear_y As Double

        shear_x = (-DOP.dir_x) / DOP.dir_z
        shear_y = (-DOP.dir_y) / DOP.dir_z

        'Setting the transformation matrix for t3
        'Shearing matrix
        set_matrix_row(T3, 0, 1, 0, 0, 0)
        set_matrix_row(T3, 1, 0, 1, 0, 0)
        set_matrix_row(T3, 2, shear_x, shear_y, 1, 0)
        set_matrix_row(T3, 3, 0, 0, 0, 1)

        'Setting the transformation matrix for t4
        'Translation matrix
        set_matrix_row(T4, 0, 1, 0, 0, 0)
        set_matrix_row(T4, 1, 0, 1, 0, 0)
        set_matrix_row(T4, 2, 0, 0, 1, 0)
        set_matrix_row(T4, 3, -CW.x, -CW.y, -fp, 1)

        Dim scale_x, scale_y, scale_z As Double

        scale_z = 1 / (fp - bp)
        scale_y = 2 / (view_windows("vmax") - view_windows("vmin"))
        scale_x = 2 / (view_windows("umax") - view_windows("umin"))

        'Setting the transformation matrix for t5
        'Scaling matrix
        set_matrix_row(T5, 0, scale_x, 0, 0, 0)
        set_matrix_row(T5, 1, 0, scale_y, 0, 0)
        set_matrix_row(T5, 2, 0, 0, scale_z, 0)
        set_matrix_row(T5, 3, 0, 0, 0, 1)

        'Setting the transformation matrix for PR2
        set_matrix_row(PR2, 0, 1, 0, 0, 0)
        set_matrix_row(PR2, 1, 0, 1, 0, 0)
        set_matrix_row(PR2, 2, 0, 0, 0, 0)
        set_matrix_row(PR2, 3, 0, 0, 0, 1)

        Dim temp_matrix1()() As Double = New Double(3)() {}
        Dim temp_matrix2()() As Double = New Double(3)() {}
        Dim final_matrix()() As Double = New Double(3)() {}

        temp_matrix1 = matrix_mult_4x4(T12, T3)
        temp_matrix2 = matrix_mult_4x4(temp_matrix1, T4)
        final_matrix = matrix_mult_4x4(temp_matrix2, T5)

        clear_all_listbox()

        'Display the content of each matrices. 
        Dim index As Integer
        Try
            For index = 0 To 3
                list_matrixt1t2.Items.Add(CStr(Decimal.Round(Convert.ToDecimal(T12(index)(0)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T12(index)(1)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T12(index)(2)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T12(index)(3)), 4)))
                list_matrixt3.Items.Add(CStr(Decimal.Round(Convert.ToDecimal(T3(index)(0)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T3(index)(1)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T3(index)(2)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T3(index)(3)), 4)))
                list_matrixt4.Items.Add(CStr(Decimal.Round(Convert.ToDecimal(T4(index)(0)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T4(index)(1)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T4(index)(2)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T4(index)(3)), 4)))
                list_matrixt5.Items.Add(CStr(Decimal.Round(Convert.ToDecimal(T5(index)(0)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T5(index)(1)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T5(index)(2)), 4)) + "  " + CStr(Decimal.Round(Convert.ToDecimal(T5(index)(3)), 4)))
            Next
        Catch ex As Exception
            'If an error occured (most probably because user inputted an invalid parameters),--
            '--Set all parameters to become the default parameters and then don't return anything;--
            '--Causing an intended error later on. 
            set_default_parameters()
            Exit Function
        End Try

        Return (final_matrix)
    End Function

    'Process vertices for clipping
    Public Function clipping_process(ByVal vertices_pr1() As TPoint, ByVal edges_main() As TLine)
        Dim index As Integer
        Dim p1, p2 As TPoint
        Dim ac_p1, ac_p2 As String
        Dim temp_arr(1) As TPoint

        Dim edges_dict As New Dictionary(Of String, List(Of New_tline))
        Dim list_front, list_back As New List(Of New_tline)
        Dim dummy_edge As New_tline

        'Iterating every edges available. 
        'edges_main is the list of edges where every edge used the first representation of edge. 
        'Since it is only used as an upper limit of a for loop, it is fine to leave it like this. 
        For index = 0 To edges_main.Length - 1
            'Get the first and second point of an edge and assign it to a variable. 
            p1 = vertices_pr1(edges_main(index).p1)
            p2 = vertices_pr1(edges_main(index).p2)

            ac_p1 = get_area_code(p1)
            ac_p2 = get_area_code(p2)

            'Checking if the edge is trivially accepted, trivially rejected, or partially accepted. 
            If ac_p1 = "000000" And ac_p2 = "000000" Then
                'Trivially accepted; assign the edge startctly to a list. 
                'dummy_edge is the new representation of an edge. 
                dummy_edge.point1 = p1
                dummy_edge.point2 = p2
                'The if statement is used to determine if an edge belongs to the front or the back side of the house. 
                If (edges_main(index).p1 = 0 And edges_main(index).p2 = 1) Or (edges_main(index).p1 = 1 And edges_main(index).p2 = 2) Or (edges_main(index).p1 = 2 And edges_main(index).p2 = 3) Or (edges_main(index).p1 = 3 And edges_main(index).p2 = 4) Or (edges_main(index).p1 = 4 And edges_main(index).p2 = 0) Then
                    list_front.Add(dummy_edge)
                Else
                    list_back.Add(dummy_edge)
                End If
            Else
                'Definitely not trivially accepted. Checking whether it is partially accepted or trivially rejected. 
                If find_logical_AND(ac_p1, ac_p2) = "000000" Then
                    'Partially Accepted; do clipping here
                    temp_arr = find_clipped_line(p1, p2, ac_p1, ac_p2)
                    dummy_edge.point1 = temp_arr(0)
                    dummy_edge.point2 = temp_arr(1)

                    'Check the trivially rejected case. 
                    'Usually after a line is clipped, it is not asured that the line is trivially accepted.
                    'It could also be trivially rejected. This if statement is to handle those possibilites. 
                    If get_area_code(dummy_edge.point1) <> "000000" And get_area_code(dummy_edge.point2) <> "000000" Then
                        Continue For
                    End If

                    If (edges_main(index).p1 = 0 And edges_main(index).p2 = 1) Or (edges_main(index).p1 = 1 And edges_main(index).p2 = 2) Or (edges_main(index).p1 = 2 And edges_main(index).p2 = 3) Or (edges_main(index).p1 = 3 And edges_main(index).p2 = 4) Or (edges_main(index).p1 = 4 And edges_main(index).p2 = 0) Then
                        list_front.Add(dummy_edge)
                    Else
                        list_back.Add(dummy_edge)
                    End If
                Else
                    'Trivially rejected; reject the whole edge; do nothing. 
                    Continue For
                End If
            End If
        Next

        'Convert the front and back list of edges to become a dictionary. 
        edges_dict.Add("front", list_front)
        edges_dict.Add("back", list_back)

        Return edges_dict
    End Function

    'THIS FUNCTION IS NOT NECESSARY NOW. 
    'Function to process edges for the trivial rejected case
    Public Function clipping_process_edges(ByVal vertices_process() As TPoint, ByVal edges_process() As TLine)
        Dim index As Integer
        Dim p1, p2 As TPoint
        Dim ac_p1, ac_p2 As String
        Dim dummy_edges(edges_process.Length - 1) As TLine
        Dim to_be_removed As New List(Of Integer)

        Array.Copy(edges_process, dummy_edges, dummy_edges.Length)

        For index = 0 To edges_process.Length - 1
            p1 = vertices_process(edges_process(index).p1)
            p2 = vertices_process(edges_process(index).p2)

            ac_p1 = get_area_code(p1)
            ac_p2 = get_area_code(p2)

            If ac_p1 <> "000000" And ac_p2 <> "000000" Then
                If find_logical_AND(ac_p1, ac_p2) <> "000000" Then
                    to_be_removed.Add(index)
                End If
            End If
        Next

        For index = to_be_removed.Count - 1 To 0 Step -1
            remove_array_element(dummy_edges, to_be_removed.Item(index))
        Next

        Return dummy_edges
    End Function

    'Function to find the clipped line between 2 points/vertices
    Public Function find_clipped_line(ByVal point1 As TPoint, ByVal point2 As TPoint, ByVal area_code1 As String, ByVal area_code2 As String)
        Dim index As Integer
        Dim cx_1, cx_2, cy_1, cy_2, cz_1, cz_2, t_1, t_2 As Double
        Dim prepare_send(1) As TPoint

        If area_code1 <> "000000" Then
            For index = 0 To 5
                If area_code1(index) = "1" Then
                    If index = 0 Then
                        'Intersection at top
                        cy_1 = 1
                        t_1 = (cy_1 - point1.y) / (point2.y - point1.y)
                        cx_1 = point1.x + (t_1 * (point2.x - point1.x))
                        cz_1 = point1.z + (t_1 * (point2.z - point1.z))
                    ElseIf index = 1 Then
                        'Intersection at bottom
                        cy_1 = -1
                        t_1 = (cy_1 - point1.y) / (point2.y - point1.y)
                        cx_1 = point1.x + (t_1 * (point2.x - point1.x))
                        cz_1 = point1.z + (t_1 * (point2.z - point1.z))
                    ElseIf index = 2 Then
                        'Intersection at right
                        cx_1 = 1
                        t_1 = (cx_1 - point1.x) / (point2.x - point1.x)
                        cy_1 = point1.y + (t_1 * (point2.y - point1.y))
                        cz_1 = point1.z + (t_1 * (point2.z - point1.z))
                    ElseIf index = 3 Then
                        'Intersection at left
                        cx_1 = -1
                        t_1 = (cx_1 - point1.x) / (point2.x - point1.x)
                        cy_1 = point1.y + (t_1 * (point2.y - point1.y))
                        cz_1 = point1.z + (t_1 * (point2.z - point1.z))
                    ElseIf index = 4 Then
                        'Intersection at front
                        cz_1 = 0
                        t_1 = (cz_1 - point1.z) / (point2.z - point1.z)
                        cy_1 = point1.y + (t_1 * (point2.y - point1.y))
                        cx_1 = point1.x + (t_1 * (point2.x - point1.x))
                    ElseIf index = 5 Then
                        'Intersection at back
                        cz_1 = -1
                        t_1 = (cz_1 - point1.z) / (point2.z - point1.z)
                        cy_1 = point1.y + (t_1 * (point2.y - point1.y))
                        cx_1 = point1.x + (t_1 * (point2.x - point1.x))
                    End If
                    point1.x = cx_1
                    point1.y = cy_1
                    point1.z = cz_1
                    'Check if the current clipped line is already trivially accepted or not. 
                    'If yes, no need for further clipping; stop the clipping process for the current point. 
                    If get_area_code(point1) = "000000" Then
                        Exit For
                    End If
                End If
            Next
        End If

        If area_code2 <> "000000" Then
            For index = 0 To 5
                If area_code2(index) = "1" Then
                    If index = 0 Then
                        'Intersection at top
                        cy_2 = 1
                        t_2 = (cy_2 - point1.y) / (point2.y - point1.y)
                        cx_2 = point1.x + (t_2 * (point2.x - point1.x))
                        cz_2 = point1.z + (t_2 * (point2.z - point1.z))
                    ElseIf index = 1 Then
                        'Intersection at bottom
                        cy_2 = -1
                        t_2 = (cy_2 - point1.y) / (point2.y - point1.y)
                        cx_2 = point1.x + (t_2 * (point2.x - point1.x))
                        cz_2 = point1.z + (t_2 * (point2.z - point1.z))
                    ElseIf index = 2 Then
                        'Intersection at right
                        cx_2 = 1
                        t_2 = (cx_2 - point1.x) / (point2.x - point1.x)
                        cy_2 = point1.y + (t_2 * (point2.y - point1.y))
                        cz_2 = point1.z + (t_2 * (point2.z - point1.z))
                    ElseIf index = 3 Then
                        'Intersection at left
                        cx_2 = -1
                        t_2 = (cx_2 - point1.x) / (point2.x - point1.x)
                        cy_2 = point1.y + (t_2 * (point2.y - point1.y))
                        cz_2 = point1.z + (t_2 * (point2.z - point1.z))
                    ElseIf index = 4 Then
                        'Intersection at front
                        cz_2 = 0
                        t_2 = (cz_2 - point1.z) / (point2.z - point1.z)
                        cy_2 = point1.y + (t_2 * (point2.y - point1.y))
                        cx_2 = point1.x + (t_2 * (point2.x - point1.x))
                    ElseIf index = 5 Then
                        'Intersection at back
                        cz_2 = -1
                        t_2 = (cz_2 - point1.z) / (point2.z - point1.z)
                        cy_2 = point1.y + (t_2 * (point2.y - point1.y))
                        cx_2 = point1.x + (t_2 * (point2.x - point1.x))
                    End If
                    point2.x = cx_2
                    point2.y = cy_2
                    point2.z = cz_2
                    If get_area_code(point2) = "000000" Then
                        Exit For
                    End If
                End If
            Next
        End If

        'Convert both end points to an array. Will be unpacked later in--
        '-- the associated code. 
        prepare_send(0) = point1
        prepare_send(1) = point2

        Return prepare_send
    End Function

    'Function to multiply both end points of an edge with a transformation matrix. 
    Public Function multiply_edge_with_matrix(ByVal some_edge As New_tline, ByVal some_matrix()() As Double)
        Dim result_edge As New_tline
        Dim temp_var(3), res_holder(3) As Double
        Dim index1, index2 As Integer
        Dim current As Double = 0

        temp_var(0) = some_edge.point1.x
        temp_var(1) = some_edge.point1.y
        temp_var(2) = some_edge.point1.z
        temp_var(3) = some_edge.point1.w

        For index1 = 0 To 3
            For index2 = 0 To 3
                current += temp_var(index2) * some_matrix(index2)(index1)
            Next
            res_holder(index1) = current
            current = 0
        Next

        result_edge.point1.x = res_holder(0)
        result_edge.point1.y = res_holder(1)
        result_edge.point1.z = res_holder(2)
        result_edge.point1.w = res_holder(3)

        ReDim temp_var(3)
        ReDim res_holder(3)

        temp_var(0) = some_edge.point2.x
        temp_var(1) = some_edge.point2.y
        temp_var(2) = some_edge.point2.z
        temp_var(3) = some_edge.point2.w

        current = 0
        For index1 = 0 To 3
            For index2 = 0 To 3
                current += temp_var(index2) * some_matrix(index2)(index1)
            Next
            res_holder(index1) = current
            current = 0
        Next

        result_edge.point2.x = res_holder(0)
        result_edge.point2.y = res_holder(1)
        result_edge.point2.z = res_holder(2)
        result_edge.point2.w = res_holder(3)

        Return result_edge
    End Function

    '=========== END FUNCTIONS ===========


    '=========== INTERACTIVITY SUB ===========
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pbox_screen.Image = btmap

        'Set the location of the initial vertices
        set_point(Vertices(0), -1, -1, 1)
        set_point(Vertices(1), 1, -1, 1)
        set_point(Vertices(2), 1, 0, 1)
        set_point(Vertices(3), 0, 1, 1)
        set_point(Vertices(4), -1, 0, 1)
        set_point(Vertices(5), -1, -1, -1)
        set_point(Vertices(6), 1, -1, -1)
        set_point(Vertices(7), 1, 0, -1)
        set_point(Vertices(8), 0, 1, -1)
        set_point(Vertices(9), -1, 0, -1)

        'Set the edges from 1 vertex to another vertex
        set_edge(edges(0), 0, 1)
        set_edge(edges(1), 1, 2)
        set_edge(edges(2), 2, 3)
        set_edge(edges(3), 3, 4)
        set_edge(edges(4), 4, 0)
        set_edge(edges(5), 5, 6)
        set_edge(edges(6), 6, 7)
        set_edge(edges(7), 7, 8)
        set_edge(edges(8), 8, 9)
        set_edge(edges(9), 9, 5)
        set_edge(edges(10), 1, 6)
        set_edge(edges(11), 2, 7)
        set_edge(edges(12), 0, 5)
        set_edge(edges(13), 4, 9)
        set_edge(edges(14), 3, 8)

        'Set the wt matrix
        set_matrix_row(wt, 0, 1, 0, 0, 0)
        set_matrix_row(wt, 1, 0, 1, 0, 0)
        set_matrix_row(wt, 2, 0, 0, 1, 0)
        set_matrix_row(wt, 3, 0, 0, 0, 1)

        'Set the vt matrix 
        set_matrix_row(vt, 0, 0.5, 0, 0, 0)
        set_matrix_row(vt, 1, 0, 0.5, 0, 0)
        set_matrix_row(vt, 2, 0, 0, 0, 0)
        set_matrix_row(vt, 3, 0, 0, 0, 1)

        'Set the st matrix
        set_matrix_row(st, 0, 100, 0, 0, 0)
        set_matrix_row(st, 1, 0, -100, 0, 0)
        set_matrix_row(st, 2, 0, 0, 0, 0)
        set_matrix_row(st, 3, 200, 200, 0, 1)

        'Setting the borders of clipping windows. 
        clip_windows.top = 1
        clip_windows.bottom = -1
        clip_windows.right = 1
        clip_windows.left = -1
        clip_windows.front = 0
        clip_windows.back = -1

        Dim index As Integer

        For index = 0 To 9
            VW(index) = matrix_mult_1x4(Vertices(index), wt)
            VR(index) = matrix_mult_1x4(VW(index), vt)
            VS(index) = matrix_mult_1x4(VR(index), st)
        Next
        set_default_parameters()

        draw_house(VS, edges)

        draw_view_windows()
    End Sub

    Private Sub Btn_default_view_Click(sender As Object, e As EventArgs) Handles btn_default_view.Click
        clear_screen()

        set_default_parameters()

        Dim index As Integer
        Dim after_pr1(9)
        Dim matrix_pr1()() As Double = New Double(3)() {}

        set_matrix_row(matrix_pr1, 0, 0.5, 0, 0, 0)
        set_matrix_row(matrix_pr1, 1, 0, 0.5, 0, 0)
        set_matrix_row(matrix_pr1, 2, 0, 0, 0.083333333333333329, 0)
        set_matrix_row(matrix_pr1, 3, 0, 0, -0.16666666666666666, 1)

        For index = 0 To 9
            VW(index) = matrix_mult_1x4(Vertices(index), wt)
            after_pr1(index) = matrix_mult_1x4(VW(index), matrix_pr1)
            VR(index) = matrix_mult_1x4(VW(index), vt)
            VS(index) = matrix_mult_1x4(VR(index), st)
        Next

        draw_house(VS, edges)

        draw_view_windows()

        clear_all_listbox()

        list_matrixt1t2.Items.Add("1  0  0  0")
        list_matrixt1t2.Items.Add("0  1  0  0")
        list_matrixt1t2.Items.Add("0  0  1  0")
        list_matrixt1t2.Items.Add("0  0  0  1")

        list_matrixt3.Items.Add("1  0  0  0")
        list_matrixt3.Items.Add("0  1  0  0")
        list_matrixt3.Items.Add("0  0  1  0")
        list_matrixt3.Items.Add("0  0  0  1")

        list_matrixt4.Items.Add("1  0  0  0")
        list_matrixt4.Items.Add("0  1  0  0")
        list_matrixt4.Items.Add("0  0  1  0")
        list_matrixt4.Items.Add("0  0  -2  1")

        list_matrixt5.Items.Add("0.5  0  0  0")
        list_matrixt5.Items.Add("0  0.5  0  0")
        list_matrixt5.Items.Add("0  0  0.0833  0")
        list_matrixt5.Items.Add("0  0  0  1")

        For index = 0 To 9
            list_vertices.Items.Add(CStr(index) + " ==> (" + CStr(after_pr1(index).x) + "," + CStr(after_pr1(index).y) + "," + CStr(after_pr1(index).z) + ")")
        Next

    End Sub

    Private Sub Btn_generate_Click(sender As Object, e As EventArgs) Handles btn_generate.Click

        clear_screen()

        grab_parameters()

        Dim new_pr1()() As Double = New Double(3)() {}
        Dim new_vt()() As Double = New Double(3)() {}
        Dim pr2()() As Double = New Double(3)() {}
        Dim points_after_pr1(9) As TPoint

        'The intended error if the user provided an invalid parameters. 
        Try
            new_pr1 = find_pr1(VRP, VPN, VUP, COP, view_windows, FP, BP)

            set_matrix_row(pr2, 0, 1, 0, 0, 0)
            set_matrix_row(pr2, 1, 0, 1, 0, 0)
            set_matrix_row(pr2, 2, 0, 0, 0, 0)
            set_matrix_row(pr2, 3, 0, 0, 0, 1)

            new_vt = matrix_mult_4x4(new_pr1, pr2)
        Catch ex As Exception
            MsgBox("Error: Invalid Parameters")
            draw_view_windows()
            draw_house(VS, edges)
            Exit Sub
        End Try

        Dim index As Integer = 0
        Dim vertices_after_clipping(9) As TPoint
        Dim edges_after_clipping(14) As TLine
        Dim after_clip, fin_clip As New Dictionary(Of String, List(Of New_tline))
        Dim init_fc, init_bc, afpr2_fc, afpr2_bc, fin_fc, fin_bc As New List(Of New_tline)

        list_vertices.Items.Clear()
        'Multiply all vertices up until all points are multiplied with pr1. 
        For index = 0 To Vertices.Length - 1
            VW(index) = matrix_mult_1x4(Vertices(index), wt)
            points_after_pr1(index) = matrix_mult_1x4(VW(index), new_pr1)
            list_vertices.Items.Add(CStr(index) + " ==> (" + CStr(points_after_pr1(index).x) + "," + CStr(points_after_pr1(index).y) + "," + CStr(points_after_pr1(index).z) + ")")
        Next

        'For index = 0 To points_after_pr1.Length - 1
        '    lbox_test.Items.Add(CStr(index) + "==>" + CStr(points_after_pr1(index).x) + " " + CStr(points_after_pr1(index).y) + " " + CStr(points_after_pr1(index).z))
        'Next
        'lbox_test.Items.Add("==================================================")

        'Do clipping here before multiplied with pr2.
        'Changing the representation of edges from the first one to become the second one. 
        after_clip = clipping_process(points_after_pr1, edges)
        init_fc = after_clip("front")
        init_bc = after_clip("back")

        'For index = 0 To vertices_after_clipping.Length - 1
        '    lbox_test.Items.Add(CStr(index) + " => " + CStr(vertices_after_clipping(index).x) + "  ,  " + CStr(vertices_after_clipping(index).y) + "  ,  " + CStr(vertices_after_clipping(index).z))
        'Next

        'edges_after_clipping = clipping_process_edges(points_after_pr1, edges)

        'For index = 0 To vertices_after_clipping.Length - 1
        '    'Later on VR will become something like this: 
        '    VR(index) = matrix_mult_1x4(vertices_after_clipping(index), pr2)
        '    VS(index) = matrix_mult_1x4(VR(index), st)
        'Next

        ''Continue the transformation until all vertices transformed into SCS for the front edges.  
        For index = 0 To init_fc.Count - 1
            afpr2_fc.Add(multiply_edge_with_matrix(init_fc.Item(index), pr2))
            fin_fc.Add(multiply_edge_with_matrix(afpr2_fc.Item(index), st))
        Next

        'Continue the transformation until all vertices transformed into SCS for the back edges. 
        For index = 0 To init_bc.Count - 1
            afpr2_bc.Add(multiply_edge_with_matrix(init_bc.Item(index), pr2))
            fin_bc.Add(multiply_edge_with_matrix(afpr2_bc.Item(index), st))
        Next

        'Transform the result (front and back edges) to become a dictionary. 
        fin_clip("front") = fin_fc
        fin_clip("back") = fin_bc

        'Draw the house according to the existing edges and vertices. 
        draw_new_house(fin_clip)

        draw_view_windows()
    End Sub

    Private Sub Btn_clear_Click(sender As Object, e As EventArgs) Handles btn_clear.Click
        clear_screen()
        clear_all_listbox()
        draw_view_windows()
        set_default_parameters()
    End Sub

    '=========== END INTERACTIVITY SUB ===========
End Class
