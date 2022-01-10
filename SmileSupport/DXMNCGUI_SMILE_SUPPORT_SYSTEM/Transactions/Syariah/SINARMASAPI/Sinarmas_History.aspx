<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Sinarmas_History.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.SINARMASAPI.Sinarmas_History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "dafault":
                    break;
            }
            cplMain.cpCallbackParam = null;
        }
    </script>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Font-Names="Calibri">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Sinarmas Polis Listing" ColCount="3">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="Transparent"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnView" ClientInstanceName="btnView" runat="server" Text="View" BackColor="LightGray" OnClick="btnView_Click" ToolTip="Click here to view document" Width="100%" Visible="false"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <%--<dx:EmptyLayoutItem Width="48%"></dx:EmptyLayoutItem>--%>
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
                                    KeyFieldName="id"
                                    Width="100%"
                                    AutoGenerateColumns="true"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    OnInit="gvMain_Init"
                                    OnDataBinding="gvMain_DataBinding"
                                    EnableTheming="True"
                                    Theme="Glass" Font-Size="Small" Font-Names="Calibri" OnCustomButtonCallback="gvMain_CustomButtonCallback" EnableCallbackAnimation="true">
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
                                        <dx:GridViewDataTextColumn Name="id" Caption="id" FieldName="id" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="LSAGREE_ID" Caption="NO KONTRAK" FieldName="LSAGREE_ID" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="NAME" Caption="NAME" FieldName="NAME" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="POLICY_NO" Caption="POLICY NO" FieldName="POLICY_NO" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="StartDate" Caption="START DATE" FieldName="StartDate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="EndDate" Caption="END DATE" FieldName="EndDate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <%--<dx:GridViewDataDateColumn Name="CON_ID" Caption="CON_ID" FieldName="CON_ID" Visible="false" ReadOnly="true" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="CASEID" Caption="CASEID" FieldName="CASEID" Visible="false" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" Width ="150px">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>--%>
                                        <%--<dx:GridViewDataTextColumn Name="PAYMENT_TOTAL" Caption="PAYMENT_TOTAL" FieldName="PAYMENT_TOTAL" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="SUM_PAYMENT_TOTAL" Caption="SUM_PAYMENT_TOTAL" FieldName="SUM_PAYMENT_TOTAL" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Name="PREMIUM" Caption="PREMIUM" FieldName="PREMIUM" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="COMMISION" Caption="COMMISION" FieldName="COMMISION" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="ADMINFEE" Caption="ADMINFEE" FieldName="ADMINFEE" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="DISCOUNT" Caption="DISCOUNT" FieldName="DISCOUNT" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="CREATED_BY" Caption="CREATED BY" FieldName="USER_ID" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="CREATED_DATE" Caption="CREATED DATE" FieldName="CREATED_DATE" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn ButtonType="Link" Caption="DOWNLOAD_PDF">
                                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="GridbtnDownload" Text=" " >
                                                    <Image Height="20px" Width="20px" Url="../../../Content/Images/download.png" ToolTip="Click here to download file."></Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
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
