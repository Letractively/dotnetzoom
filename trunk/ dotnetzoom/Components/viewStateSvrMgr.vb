Imports System
Imports System.Collections
Imports System.Configuration

Namespace DotNetZoom

    Public Class viewStateSvrMgr

        Private Const VIEW_STATE_NUM_PAGES As Integer = 5  '//Number of pages to keep viewstate for

        '//Name of storage location for veiwstate information
        Private Const SESSION_VIEW_STATE_MGR As String = "VIEW_STATE_MGR"

        Private lPageCount As Long = 0                      '//Number of pages seen by this customer 
        Private ViewStates(VIEW_STATE_NUM_PAGES) As String  '//Store for viewstates




        Public Function SaveViewState(ByVal szViewState As String) As Long
            '//Increment the total page seen counter
            lPageCount += 1

            '// Now use the modulas operator (%) to find remainder of that and size of viewstate
            '// storage, this creates a circular array where it continually cycles through the
            '// array index range (effectively keeps the last requests to match size of storage)
            Dim siIndex As Short = (lPageCount Mod VIEW_STATE_NUM_PAGES)

            '//Now save the viewstate for this page to the current position.  
            ViewStates(siIndex) = szViewState
            SaveViewState = lPageCount
        End Function

        Public Function GetViewState(ByVal lRequestNumber As Long) As String
            '// Could cycle though the array and make sure that the given request number
            '// is actually present (in case the array is not big enough).  Much faster
            '// to just take the given request number and recalculate where it should be
            '// stored
            Dim siIndex As Short = (lRequestNumber Mod VIEW_STATE_NUM_PAGES)
            GetViewState = ViewStates(siIndex)
        End Function

        Public Shared Function GetViewStateSvrMgr() As viewStateSvrMgr
            Dim oViewStateMgr As viewStateSvrMgr

            '//Check if already created the order object in session
            If (System.Web.HttpContext.Current.Session(SESSION_VIEW_STATE_MGR) Is Nothing) Then
                '//Not already in session, create a new one and put in session
                oViewStateMgr = New viewStateSvrMgr
                System.Web.HttpContext.Current.Session(SESSION_VIEW_STATE_MGR) = oViewStateMgr
            Else
                '//Return the session order
                oViewStateMgr = CType(System.Web.HttpContext.Current.Session(SESSION_VIEW_STATE_MGR), viewStateSvrMgr)
            End If
            GetViewStateSvrMgr = oViewStateMgr
        End Function

        Public Sub New()
            'new
        End Sub

    End Class

End Namespace