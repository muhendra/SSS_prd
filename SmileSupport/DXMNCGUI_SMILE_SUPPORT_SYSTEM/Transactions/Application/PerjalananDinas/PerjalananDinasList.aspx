<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PerjalananDinasList.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.PerjalananDinas.PerjalananDinasList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "dafault":
                    break;
            }
            cplMain.cpCallbackParam = null;
        }

        function gvApprovalList_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShow":
                    cplMain.PerformCallback("SHOW;SHOW");
                    break;
            }
        }

        function gvRealisasiApprovalList_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShowRealisasi":
                    cplMain.PerformCallback("SHOW_REALISASI;SHOW_REALISASI");
                    break;
            }
        }

    </script>
    <dx:ASPxHiddenField  runat="server" ID="HiddenField" ClientInstanceName="HiddenField">
    </dx:ASPxHiddenField>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcApproval" ClientInstanceName="apcApproval" runat="server" Modal="True" Theme="Aqua" Width="1050px" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Your approval list.." AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <SettingsAdaptivity Mode="OnWindowInnerWidth" MaxHeight="100%" MaxWidth="100%"/>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxFormLayout runat="server">
                    <Items>
                        <dx:LayoutItem ShowCaption="False" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView 
                                        ID="gvApprovalList"
                                        ClientInstanceName="gvApprovalList"
                                        runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" 
                                        OnDataBinding="gvApprovalList_DataBinding" OnCustomCallback="gvApprovalList_CustomCallback"
                                        Theme="Aqua" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                        <ClientSideEvents CustomButtonClick="function(s, e) { gvApprovalList_CustomButtonClick(s, e); }"/>
                                        <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="false" ShowFilterBar="Hidden" ShowHeaderFilterButton="true" ShowFooter="false" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                        <SettingsLoadingPanel Mode="Disabled" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colNik" Caption="NIK" FieldName="NIK" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colClientName" Caption="Name" FieldName="Name" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTypeApproval" Caption="Type Approval" FieldName="TypeApproval" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTanggalPengajuan" Caption="Tanggal Pengajuan" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAsal" Caption="Asal Keberangkatan" FieldName="FromTujuan" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTujuan" Caption="Tujuan Keberangkatan" FieldName="Tujuan" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="4" Caption=" " >
                                                <HeaderStyle Font-Bold="true" />
                                                <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnShow" Text="Show"></dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <Styles>
                                            <AlternatingRow Enabled="True"></AlternatingRow>
                                        </Styles>
                                        <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="apcRealisasi" ClientInstanceName="apcRealisasi" runat="server" Modal="True" Theme="Aqua" Width="1050px" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Your approval list.." AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <SettingsAdaptivity Mode="OnWindowInnerWidth" MaxHeight="100%" MaxWidth="100%"/>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxFormLayout runat="server">
                    <Items>
                        <dx:LayoutItem ShowCaption="False" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView 
                                        ID="gvRealisasiApprovalList"
                                        ClientInstanceName="gvRealisasiApprovalList"
                                        runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" 
                                        OnDataBinding="gvRealisasiApprovalList_DataBinding" OnCustomCallback="gvRealisasiApprovalList_CustomCallback"
                                        Theme="Aqua" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                        <ClientSideEvents CustomButtonClick="function(s, e) { gvRealisasiApprovalList_CustomButtonClick(s, e); }"/>
                                        <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="false" ShowFilterBar="Hidden" ShowHeaderFilterButton="true" ShowFooter="false" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                        <SettingsLoadingPanel Mode="Disabled" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colNik" Caption="NIK" FieldName="NIK" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="2" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colClientName" Caption="Name" FieldName="Name" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTypeApproval" Caption="Type Approval" FieldName="TypeApproval" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTanggalPengajuan" Caption="Tanggal Pengajuan" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colAsal" Caption="Asal Keberangkatan" FieldName="FromTujuan" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTujuan" Caption="Tujuan Keberangkatan" FieldName="Tujuan" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" >
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="4" Caption=" " >
                                                <HeaderStyle Font-Bold="true" />
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnShowRealisasi" Text="Show"></dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <Styles>
                                            <AlternatingRow Enabled="True"></AlternatingRow>
                                        </Styles>
                                        <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Perjalanan Dinas Listing" ColCount="7">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxButton ID="btnNew" ClientInstanceName="btnNew" runat="server" Text="New" BackColor="LightGray" OnClick="btnNew_Click" ToolTip="Click here to create a new document" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnView" ClientInstanceName="btnView" runat="server" Text="View" BackColor="LightGray" OnClick="btnView_Click" ToolTip="Click here to view document" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <dx:ASPxButton ID="btnEdit" ClientInstanceName="btnEdit" runat="server" Text="Edit" BackColor="LightGray" OnClick="btnEdit_Click" ToolTip="Click here to edit document" Width="100%"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="45%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                <dx:ASPxButton ID="btnApprovalList" ClientInstanceName="btnApprovalList" runat="server" Text="Approval List" BackColor="LightGray" AutoPostBack="false" OnClick="btnApprovalList_Click" ToolTip="Click here to view approval list" Width="100%">
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                <dx:ASPxButton ID="btnRealisasiList" ClientInstanceName="btnRealisasiList" runat="server" Text="Realisasi" BackColor="LightGray" AutoPostBack="false" OnClick="btnRealisasiList_Click" ToolTip="Click here to view Realisasi list" Width="100%">
                                    
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>

            <dx:LayoutGroup ShowCaption="False" ColCount="1" GroupBoxDecoration="None">
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvMain"
                                    ClientInstanceName="gvMain"
                                    runat="server"
                                    KeyFieldName="DocKey"
                                    Width="100%"
                                    AutoGenerateColumns="true"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
                                    OnDataBinding="gvMain_DataBinding"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" EnableCallbackAnimation="true">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsPager PageSize="10"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live"/>
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText" />
                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDocNo" Caption="DocNo" FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" Width ="100px">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colDocDate" Caption="DocDate" FieldName="DocDate" Visible="false" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="Status" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" Width ="150px">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colNik" Caption="NIK" FieldName="NIK" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" Width="80px">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colName" Caption="Name" FieldName="Name" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDept" Caption="Dept" FieldName="Dept" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colJabatan" Caption="Jabatan" FieldName="Jabatan" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colTujuan" Caption="Tujuan" FieldName="Tujuan" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colPembebananBiaya" Caption="Pembebanan Biaya" FieldName="PembebananBiaya" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colcre_by" Caption="Created By" FieldName="CRE_BY" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCreatedDateTime" Caption="Tanggal Pengajuan" FieldName="CRE_DATE" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colModifiedBy" Caption="Modified By" FieldName="MOD_BY" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colModifiedDate" Caption="Modified Date" FieldName="MOD_DATE" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <Styles>
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                    </Styles>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                    <SettingsDetail ShowDetailRow="false" />
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>

        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
