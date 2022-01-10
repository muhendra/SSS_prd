<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" Async="true" AutoEventWireup="true" CodeBehind="SINARMAS_All.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.SINARMASAPI.SINARMAS_All" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "LOAD":
                    gvTempData.Refresh();
                    gvDetailresponse.Refresh();
                    //if (cplMain.cpEnableBtn == 'enable') {
                    //    btnProgress.SetVisible(true);
                    //} else {
                    //    btnProgress.SetVisible(false);
                    //}
                    break;
            }
            cplMain.cpCallbackParam = null;
        }
        function OnClientChanged(ccode) {
            var grid = gvClient.GetGridView();
            if (gvClient.GetText() != "") {
                cplMain.PerformCallback("LOAD;" + gvClient.GetValue().toString());
            }
        }
        function OnDebGridFocusedRowChanged() {
            
        }
        function OnTempGridFocusedRowChanged() {
            gvTempData.GetRowValues(gvTempData.GetFocusedRowIndex(), 'CLIENT;SID_PENGURUSID', OnTempGetRowValues);
        }
        function OnTempGetRowValues(values) {
            //cplMain.PerformCallback("DEB;" + values[0].toString() + ";" + values[1].toString());
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server">
                    <Items>
                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None" ColCount="2">
                            <Items>
                                <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                            <div style="text-align:center">
                                                <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="Are you sure ?" Width="100%">
                                                </dx:ASPxLabel>
                                            </div>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:EmptyLayoutItem ColSpan="2"></dx:EmptyLayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                            <dx:ASPxButton ID="btnRequestConfirm" ClientInstanceName="btnRequestConfirm" runat="server" Text="OK" AutoPostBack="true" OnClick="btnRequestConfirm_Click" Width="100" Theme="Office2010Black">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); lp.Show(); } " />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                            <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100" Theme="Office2010Black">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Width="250px">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout" ClientInstanceName="ASPxFormLayout" runat="server" Theme="Glass" Width="100%">
        <Items>
            <dx:LayoutGroup Name="LayoutGroupClient" ShowCaption="True" Caption="Penerbitan Polis Asuransi" GroupBoxDecoration="Box" ColCount="1" >
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                    <dx:TabbedLayoutGroup Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0">
                        <Items>
                            <dx:LayoutGroup Caption="Penerbitan Polis">
                                <Items>
                                    <dx:LayoutItem ShowCaption="True" Caption="Cari Kontrak" CaptionStyle-Font-Bold="false">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridLookup runat="server" ID="gvClient" ClientInstanceName="gvClient" KeyFieldName="LSAGREE" OnDataBinding="gvClient_DataBinding" DisplayFormatString="{0}" TextFormatString="{0}" MultiTextSeparator=";" Width="300px" NullText="--Select--">
                                                    <ClientSideEvents ValueChanged="function(s, e) { OnClientChanged(s); }"/>
                                                    <GridViewProperties EnablePagingCallbackAnimation="true">
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True"/>
                                                    </GridViewProperties>
                                                </dx:ASPxGridLookup>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                    <dx:LayoutItem ShowCaption="False" Width="50%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    ID="gvTempData"
                                                    runat="server"
                                                    ClientInstanceName="gvTempData"
                                                    OnDataBinding="gvTempData_DataBinding"
                                                    Width="100%">
                                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="true"/>
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) { OnTempGridFocusedRowChanged(); }"/>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="BusinessCode" Caption="Business Code" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="BusinessName" Caption="Business Name" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="GroupPanel" Caption="Group Panel" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="AccessCode" Caption="Access Code" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="LSAGREE" Caption="No. Kontrak" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="pyFullName" Caption="Full Name" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="pyFirstName" Caption="First Name" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="pyLastName" Caption="Last Name" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMDateOfBirth" Caption="Date Of Birth" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMGender" Caption="Gender" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMIDCARD" Caption="ID CARD" Visible="false" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="pyCompany" Caption="Company" Visible="false" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="st_date" Caption="Start Date" Width="100"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="en_date" Caption="End Date" Width="100"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="QQName" Caption="QQName" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CustomerType" Caption="CustomerType" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TheInsured" Caption="TheInsured" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="IdTransaction" Caption="IdTransaction" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TripType" Caption="TripType" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Currency" Caption="Currency" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TypeOfPacket" Caption="TypeOfPacket" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="StatusPenerbitan" Caption="StatusPenerbitan" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMAddress" Caption="Address" Width="500px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMZipCode" Caption="Zip Code" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TelFaxCode" Caption="TelFaxCode" Visible="false" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TelFaxNumber" Caption="Tel/Fax Number" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TelFaxType" Caption="TelFaxType" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMDateOfBirth_P" Caption="ASMDateOfBirth_P" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMHeight" Caption="ASMHeight" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMIDCard" Caption="ASMIDCard" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMJobName" Caption="Job Name" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMLeftHanded" Caption="is_LeftHanded" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="TSI_Nominal" Caption="Total Sum Insured" Visible="true"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMParticipantStatus" Caption="Participant Status" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMWeight" Caption="Weight" Visible="false" ></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="StartDateTime_P" Caption="StartDateTime_P" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="EndDateTime_P" Caption="EndDateTime_P" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="pyFullName" Caption="pyFullName" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Coverage" Caption="Coverage" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CoverageNote" Caption="CoverageNote" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="DiscountPercentage" Caption="DiscountPercentage" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CalculateMethod" Caption="CalculateMethod" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="Rate" Caption="Rate" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMGender_P" Caption="ASMGender_P" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMHeirPercentage" Caption="ASMHeirPercentage" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ASMRelationName" Caption="ASMRelationName" Visible="false"></dx:GridViewDataTextColumn>
                                                        <%--<dx:GridViewDataTextColumn FieldName="pyFullName" Caption="pyFullName" ></dx:GridViewDataTextColumn>--%>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                    <dx:LayoutItem ShowCaption="false">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton runat="server" ID="btnProgress" ClientInstanceName="btnProgress" Height="30px" Text="Request Penerbitan Polis" AutoPostBack="false"  Theme="MaterialCompact" ClientVisible="true">
                                                    <ClientSideEvents Click="function(s, e) { apcconfirm.Show(); }" />
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxLabel ID="lblHasilCheck" runat="server" Text="Response Penerbitan Polis :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem ShowCaption="False" Width="50%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    ID="gvDetailresponse"
                                                    runat="server"
                                                    ClientInstanceName="gvDetailresponse"
                                                    OnDataBinding="gvDetailresponse_DataBinding"
                                                    Width="100%">
                                                    <Settings HorizontalScrollBarMode="Visible" />

                                                    <SettingsBehavior AllowDragDrop="false" AllowSort="false" AllowFocusedRow="false"/>
                                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                                    <Toolbars>
                                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                                            <Items>
                                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel" />
                                                            </Items>
                                                        </dx:GridViewToolbar>
                                                    </Toolbars>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="LSAGREE_ID" Caption="No Kontrak" Width="150px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="POLICY_NO" Caption="Policy No" Width="150px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Name" Width="150px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="StartDate" Caption="Start Date" Width="125px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="EndDate" Caption="End Date" Width="125px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PREMIUM" Caption="Premium" Width="125px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ADMINFEE" Caption="Admin Fee" Width="150px" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="COMMISION" Caption="Commision" Width="150px" Visible="false"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="USER_ID" Caption="USER_ID" Width="100px"></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="CREATED_DATE" Caption="Created Date" Width="150px"></dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                    <dx:LayoutItem ShowCaption="false">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnRefresh" ClientInstanceName="btnRefresh" runat="server" Height="30px" Text="Refresh"  OnClick="btnRefresh_Click" Theme="MaterialCompact" Visible="true"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:TabbedLayoutGroup>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" BackgroundImage-HorizontalPosition="center" Theme="Glass" Text="Loading Please Wait...">
        <BackgroundImage VerticalPosition="top" />
    </dx:ASPxLoadingPanel>
</asp:Content>
