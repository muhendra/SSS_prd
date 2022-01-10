<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PerjalananDinasDoc.aspx.cs" Inherits="DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.PerjalananDinas.PerjalananDinasDoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function cplMain_EndCallback(s, e) {
            switch (cplMain.cpCallbackParam) {
                case "dafault":
                    break;

                case "SAVE_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "SUBMIT_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }

                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;

                case "SAVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;

                case "SUBMIT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;

                case "UPLOADCONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        
                        break;
                    }

                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;

                case "UPLOAD":
                    gvMain.Refresh();
                    tbLayoutGroup.SetActiveTabIndex(3);
                    UploadCtrl.ClearText();
                    documentTypeSPD.SetValue("");
                    mmNotes.SetValue("");
                    UploadCtrl.SetVisible(true);
                    btnDownload.SetVisible(false);

                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();

                    //window.location.reload(true);
                    break;

                case "APPROVE_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }

                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;

                case "APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;

                case "REJECT_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }

                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }

                    break;
                case "REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;

                case "CANCEL":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;

                case "CALCULATE":
                    mandays1.SetValue(cplMain.cpT_MANDAYS);
                    tunjanganMakan.SetValue(cplMain.cpT_MAKAN);
                    uangSaku.SetValue(cplMain.cpT_UANGSAKU);
                    toPNJ.SetValue(s.cpT_TOTALPENGAJUAN);
                    
                    break;

                case "CALCULATE_REALISASI":
                    mandays2.SetValue(cplMain.cpT_MANDAYS);
                    tunjanganMakan.SetValue(cplMain.cpT_MAKAN);
                    uangSaku.SetValue(cplMain.cpT_UANGSAKU);
                    toREL.SetValue(s.cpT_TOTALREALISASI);
                    break;
            }

            
            cplMain.cpCallbackParam = null;
        }

        function gvDetail_EndCallback(s, e) {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE") {

                toPNJ.SetText("0");

                toPNJ.SetText(s.cpTotal);
                toPNJ.GetInputElement().readOnly = true;
                
                calculationNTF();
                s.cpCmd = "";
            }
        }

        function gvRealisasiPerjalananDinas_EndCallback(s, e) {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE") {
                toREL.SetText("0");

                toREL.SetText(s.cpTotal2);
                toREL.GetInputElement().readOnly = true;

                calcREL();
                s.cpCmd2 = "";
            }
        }

        function calculationNTF() {
            var vOTR = parseFloat(0.0);
            if (toPNJ.GetValue() != null && toPNJ.GetValue().toString() != "" && toPNJ.GetValue().toString().length != 0) {
                vOTR = parseFloat(toPNJ.GetValue().toString());
            }
        }

        function calcREL() {
            var vREL = parseFloat(0.0);
            if (toREL.GetValue() != null && toREL.GetValue().toString() != "" && toREL.GetValue().toString().length != 0) {
                vREL = parseFloat(toREL.GetValue().toString());
            }
        }

        function gvApproval_EndCallback(s, e) {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE") {
                s.cpCmd = "";
            }
        }

        function gvDetailRealisasi_EndCallback(s, e) {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE") {
                s.cpCmd = "";
            }
        }

        function gvApprovalRealisasi_EndCallback(s, e) {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE") {
                s.cpCmd = "";
            }
        }

        function onUploadControlFileUploadComplete(s, e) {
            UploadCtrl.SetVisible(false);
            btnDownload.SetVisible(true);
        }

        function OnNameChanged(s, e) {
            gvApproval.GetEditor("colNIK").SetValue(s.GetSelectedItem().GetColumnText('NIK'));
            gvApproval.GetEditor("colEmail").SetValue(s.GetSelectedItem().GetColumnText('Email'));
        }

        function OnNameChangedRel(s, e) {
            gvApprovalRealisasi.GetEditor("colNIK").SetValue(s.GetSelectedItem().GetColumnText('NIK'));
            gvApprovalRealisasi.GetEditor("colEmail").SetValue(s.GetSelectedItem().GetColumnText('Email'));
        }

        function onUploadDocNewUploadComplete(s, e) {
            UploadCtrlNew.SetVisible(false);
            btnDownloadNew.SetVisible(true);
        }

    </script>
    <dx:ASPxHiddenField runat="server" ID="ASPxHiddenField1" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True" Theme="MetropolisBlue" EnableCallbackAnimation="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="we need your confirmation.." AllowDragging="False" PopupAnimationType="Fade" EnableViewState="False" CloseAction="CloseButton">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                    <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                    <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); apcconfirm.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                    <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
                                        <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" Theme="MetropolisBlue" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="None"
        EnableViewState="False" Width="400" Height="75">
    </dx:ASPxPopupControl>
     <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" ClientInstanceName="ASPxFormLayout" Theme="Glass">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Name="LayoutGroupHeader" ShowCaption="True" Caption="Header Perjalanan Dinas" ColCount="1" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem Name="SPDNo" ShowCaption="true" Caption="SPD No" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="SPDNo" ClientInstanceName="SPDNo" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Name" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtName" ClientInstanceName="txtName" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="NIK" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="nik_emp" ClientInstanceName="nik_emp" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                   <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem Name="pos_emp" ShowCaption="true" Caption="Jabatan / Golongan" Width="400px" Height="20px" >
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="pos_emp" runat="server" ClientInstanceName="pos_emp" Width="100%" NullText="-- Select --">
                                   <%-- <Items>
                                        <dx:ListEditItem Text="Non-Officer" Value="Non Officer"/>
                                        <dx:ListEditItem Text="Officer" Value="Officer" Selected="true"/>
                                        <dx:ListEditItem Text="SPV/Asst. Manager" Value="SPV/Asst. Manager"/>
                                        <dx:ListEditItem Text="Manager/Senior Manager" Value="Manager/Senior Manager"/>
                                        <dx:ListEditItem Text="GM/VP" Value="GM/VP"/>
                                        <dx:ListEditItem Text="SVP/EVP" Value="SVP/EVP"/>
                                        <dx:ListEditItem Text="Direktur" Value="Direktur"/>
                                    </Items>--%>
                                    <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { OnWaiveChanged(s); }" />--%>
                                    <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('CALCULATE;'); }"/>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem Name="pos_emp3" ShowCaption="true" Caption="Jabatan / Golongan" Width="400px" Height="20px" >
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="pos_emp3" runat="server" ClientInstanceName="pos_emp3" Width="100%" NullText="-- Select --">
                                   <%-- <Items>
                                        <dx:ListEditItem Text="Non-Officer" Value="Non Officer"/>
                                        <dx:ListEditItem Text="Officer" Value="Officer" Selected="true"/>
                                        <dx:ListEditItem Text="SPV/Asst. Manager" Value="SPV/Asst. Manager"/>
                                        <dx:ListEditItem Text="Manager/Senior Manager" Value="Manager/Senior Manager"/>
                                        <dx:ListEditItem Text="GM/VP" Value="GM/VP"/>
                                        <dx:ListEditItem Text="SVP/EVP" Value="SVP/EVP"/>
                                        <dx:ListEditItem Text="Direktur" Value="Direktur"/>
                                    </Items>--%>
                                    <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { OnWaiveChanged(s); }" />--%>
                                    <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('CALCULATE;'); }"/>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Pembebanan Biaya" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="spd_charge" ClientInstanceName="spd_charge" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="PT/Dept" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="spd_dept" ClientInstanceName="spd_dept" ClientEnabled ="true">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Name="empt_sspd" Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem Name="status_spd" ShowCaption="false" Caption="Status" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="status_spd" ClientInstanceName="status_spd" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Name="DocNo" ShowCaption="false" Caption="DocNo" Width="30%">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="DocNo" ClientInstanceName="DocNo" ClientEnabled ="false">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    
                    <dx:EmptyLayoutItem Name ="emptyLay4" Height ="4px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem Name="pos_emp2" ShowCaption="false" Caption="Jabatan / Golongan" Width="400px" Height="20px" >
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="pos_emp2" runat="server" ClientInstanceName="pos_emp2" Width="100%" NullText="-- Select --" ClientEnabled ="false">
                                    <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('CALCULATE;'); }"/>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupRencana" ShowCaption="True" Caption="Rencana Perjalanan Dinas" GroupBoxDecoration="Box" ColCount="1" Width="50%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="true" Caption="Tanggal Keberangkatan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="tglKeberangkatan" ClientInstanceName="tglKeberangkatan" Width="100%" DisplayFormatString="dd/MM/yyyy HH:mm:ss" ClientEnabled="true" NullText="-- Select --">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss"></TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClientSideEvents DateChanged="function(s,e) { cplMain.PerformCallback('CALCULATE;'); }" />
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Tanggal Kepulangan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="tglKepulangan" ClientInstanceName="tglKepulangan" Width="100%" DisplayFormatString="dd/MM/yyyy HH:mm:ss" ClientEnabled="true" NullText="-- Select --">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss"></TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClientSideEvents DateChanged="function(s,e) { cplMain.PerformCallback('CALCULATE;'); }" />
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem Name="Mandays1" ShowCaption="true" Caption="Jumlah Hari" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="mandays1" runat="server" ClientInstanceName="mandays1" Number="0" DisplayFormatString="{0:#,0}" Width="100%" ClientEnabled="false" NullText="0">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Name="mandaysShdw" ShowCaption="true" Caption="Jumlah Hari" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="mandays3" runat="server" ClientInstanceName="mandays3" Number="0" DisplayFormatString="{0:#,0}" Width="100%" ClientEnabled="false" NullText="0">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Asal Keberangkatan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="negaraasal1" ClientInstanceName="negaraasal1" ClientEnabled ="true" NullText="..." ValidationGroup="ValidationSave">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Kota/Negara Tujuan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="negaratujuan1" ClientInstanceName="negaratujuan1" ClientEnabled ="true" NullText="...">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>               
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Kendaraan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="byvehicle1" ClientInstanceName="byvehicle1" ClientEnabled ="true" NullText="...">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupRealisasi" ShowCaption="True" Caption="Realisasi Perjalanan Dinas" GroupBoxDecoration="Box" ColCount="1" Width="50%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                <Items>
                <%--<dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Keberangkatan :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                </dx:LayoutItem>     
                    <dx:EmptyLayoutItem Width="120%"></dx:EmptyLayoutItem>--%>
                    <dx:LayoutItem ShowCaption="true" Caption="Tanggal Keberangkatan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="tglKeberangkatan2" ClientInstanceName="tglKeberangkatan2" Width="100%" DisplayFormatString="dd/MM/yyyy HH:mm:ss" ClientEnabled="true" NullText="-- Select --">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss"></TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClientSideEvents DateChanged="function(s,e) { cplMain.PerformCallback('CALCULATE_REALISASI;'); }" />
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Tanggal Kepulangan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="tglKepulangan2" ClientInstanceName="tglKepulangan2" Width="100%" DisplayFormatString="dd/MM/yyyy HH:mm:ss" ClientEnabled="true" NullText="-- Select --">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss"></TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClientSideEvents DateChanged="function(s,e) { cplMain.PerformCallback('CALCULATE_REALISASI;'); }" />
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Jumlah Hari" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit ID="mandays2" runat="server" ClientInstanceName="mandays2" Number="0" DisplayFormatString="{0:#,0}" Width="100%" ClientEnabled="true" NullText="0">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Asal Keberangkatan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="negaraasal2" ClientInstanceName="negaraasal2" ClientEnabled ="true" NullText="..." >
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Kota/Negara Tujuan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="negaratujuan2" ClientInstanceName="negaratujuan2" ClientEnabled ="true" NullText="...">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>               

                <%--<dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="

                            Kepulangan :" Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray"></dx:ASPxLabel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
                </dx:LayoutItem>--%>
                    
                    <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="Kendaraan" Width="400px" Height="20px">
                        <CaptionSettings Location="Left"/>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="byvehicle2" ClientInstanceName="byvehicle2" ClientEnabled ="true" NullText="...">
                                    <HelpTextSettings DisplayMode="Popup"></HelpTextSettings>
                                    <HelpTextStyle ForeColor="DarkGreen"></HelpTextStyle>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="calculate_spd" ClientInstanceName="calculate_spd" Text="Calculate" AutoPostBack="false" Theme="Glass" OnClick="Calculate_spd" ClientVisible ="false">
                            <%--<ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE_CONFIRM;' + 'SAVE_CONFIRM;'); }" />--%>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutGroup Name="LayoutGroupTunjangan" GroupBoxDecoration="Box" Caption="Type Tunjangan" ColCount="1" GroupBoxStyle-Border-BorderColor="#d1ecee" Width="100%">
                <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true" Caption-Font-Size="Small"></GroupBoxStyle>
                        <Items>
                            <%--<dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="type_tjg" runat="server" Text="Type Tunjangan :
                                            " Font-Bold="true" Font-Names="Calibri" ForeColor="SlateGray" Font-Size ="Small"></dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>--%> 
                            <dx:EmptyLayoutItem Height="10px"></dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Tunjangan Makan">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="tunjanganMakan" ClientInstanceName="tunjanganMakan" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" ReadOnly="true" Font-Bold="true" ForeColor="#666666" Width="300px" Height="20px">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Height="2px"></dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Uang Saku">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="uangSaku" ClientInstanceName="uangSaku" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666" Width="300px" Height="20px">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
 <dx:LayoutGroup Name="LayoutGroupUploadDoc" GroupBoxDecoration="Box" Caption="" ColCount="3" GroupBoxStyle-Border-BorderColor="#F8FAFD" Width="100%">
	<Items>
		<dx:LayoutItem Caption="Upload Document Pengajuan">
			<LayoutItemNestedControlCollection>
				<dx:LayoutItemNestedControlContainer>
					<dx:ASPxUploadControl runat="server" ID="UploadCtrlNew" ClientInstanceName="UploadCtrlNew" ShowProgressPanel="true" UploadMode="Auto" AutoStartUpload="true" FileUploadMode="OnPageLoad"
						OnFileUploadComplete="UploadCtrl_FileUploadComplete" Width="240px" Height="20px">
						<ValidationSettings MaxFileSize="10000000" ErrorStyle-BackColor="Red" ShowErrors="true" AllowedFileExtensions=".pdf,.jpg,.jpeg,.img,.tiff,.xls,.xlsx,.txt,.doc,.docx,.rar,.zip">
							<ErrorStyle BackColor="Red"></ErrorStyle>
						</ValidationSettings>
						<ClientSideEvents FileUploadComplete="onUploadDocNewUploadComplete" />
					</dx:ASPxUploadControl>
					<dx:ASPxButton ID="btnDownloadNew" runat="server" ClientInstanceName="btnDownloadNew" OnClick="btnDownload_Click" Text="View Document" ClientVisible="false" Width="35%" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Left" VerticalAlign="Middle">

					</dx:ASPxButton>
				</dx:LayoutItemNestedControlContainer>
			</LayoutItemNestedControlCollection>
		</dx:LayoutItem>
		<dx:EmptyLayoutItem></dx:EmptyLayoutItem>
	</Items>
</dx:LayoutGroup>
            <dx:TabbedLayoutGroup Height="200px" Name="tbLayoutGroupDetail" ClientInstanceName="tbLayoutGroup" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                        <Items>
                            <dx:LayoutGroup Caption="Rincian Anggaran Perjalanan Dinas">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                                <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvDetail"
                                                    ClientInstanceName="gvDetail"
                                                    Width="100%"
                                                    KeyFieldName="DtlKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Glass"
                                                    Font-Names="Calibri"
                                                    OnInit="gvDetail_Init"
                                                    OnInitNewRow="gvDetail_InitNewRow"
                                                    OnDataBinding="gvDetail_DataBinding"
                                                    OnRowInserting="gvDetail_RowInserting"
                                                    OnRowUpdating="gvDetail_RowUpdating"
                                                    OnRowDeleting="gvDetail_RowDeleting"
                                                    OnCustomCallback="gvDetail_CustomCallback"
                                                    OnAutoFilterCellEditorInitialize="gvDetail_AutoFilterCellEditorInitialize"
                                                    OnCustomColumnDisplayText="gvDetail_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents EndCallback="gvDetail_EndCallback" />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px">
                                                        </NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="True" Visible="false" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colTypeSPD" Caption="TypeSPD" FieldName="TypeSPD" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="5">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="typeTunjangan" Caption="Type Tunjangan" FieldName="TypeBudget" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="6" >
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesComboBox ClientInstanceName="colCondition"
                                                                TextField="Condition" ValueField="Condition" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Transportasi" Value="Transportasi" />
                                                                    <dx:ListEditItem Text="Penginapan" Value="Penginapan" />
                                                                    <dx:ListEditItem Text="Biaya Lain-Lain" Value="Biaya Lain-Lain" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colDesc" FieldName="BudgetDesc" Caption="Description" VisibleIndex="7">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn
                                                            Name="colSubTotal"
                                                            Caption="Sub Total Perjalanan Dinas"
                                                            FieldName="BudgetAmount"
                                                            ShowInCustomizationForm="True"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}"
                                                            PropertiesSpinEdit-DisplayFormatInEditMode="true"
                                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                                            PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false"
                                                            PropertiesSpinEdit-MinValue="-9999999999999"
                                                            PropertiesSpinEdit-MaxValue="999999999999999"
                                                            VisibleIndex="8"
                                                            Width="35%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Name="lgDetailApproval" Caption="Approval Pengajuan Perjalanan Dinas" GroupBoxStyle-Caption-BackColor="White" ColCount="4" Visible="True">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvApproval"
                                                    ClientInstanceName="gvApproval"
                                                    Width="100%"
                                                    KeyFieldName="DtlKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Aqua"
                                                    Font-Names="Calibri"
                                                    OnDataBinding="gvApproval_DataBinding"
                                                    OnRowInserting="gvApproval_RowInserting"
                                                    OnRowUpdating="gvApproval_RowUpdating"
                                                    OnRowDeleting="gvApproval_RowDeleting"
                                                    OnCustomCallback="gvApproval_CustomCallback"
                                                    OnAutoFilterCellEditorInitialize="gvApproval_AutoFilterCellEditorInitialize" OnCellEditorInitialize="gvApproval_CellEditorInitialize"
                                                    OnCustomColumnDisplayText="gvApproval_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents EndCallback="gvApproval_EndCallback" />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px">
                                                        </NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" VisibleIndex="0" Width="2%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlAppKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="True" Visible="false" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colNIK" FieldName="NIK" ShowInCustomizationForm="True" Caption="NIK" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colNama" FieldName="Nama" ShowInCustomizationForm="True" Caption="Nama" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesComboBox EnableCallbackMode="true" ClientInstanceName="colNama" DropDownRows="10" IncrementalFilteringDelay="500" IncrementalFilteringMode="Contains" DisplayFormatString="{1}" TextFormatString="{1}" DropDownStyle="DropDownList" ValueField="Nama" TextField="Nama" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e) {OnNameChanged(s);}" />
                                                                <ItemStyle Wrap="True"></ItemStyle>
                                                                <Columns>
                                                                    <dx:ListBoxColumn FieldName="NIK" Caption="NIK" Width="150px" />
                                                                    <dx:ListBoxColumn FieldName="Nama" Caption="Nama" Width="150px" />
                                                                    <dx:ListBoxColumn FieldName="Email" Caption="Email" Width="150px"/>
                                                                </Columns>
                                                                <ValidationSettings ValidateOnLeave="true" ValidationGroup="ValidationSave" Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                                    <RequiredField ErrorText="* Value can't be empty." IsRequired="true" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colJabatan" FieldName="Jabatan" Caption="Jabatan" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataCheckColumn Name="colIsDecision" FieldName="IsDecision" ShowInCustomizationForm="True" Caption="Decision?" ReadOnly="true" Width="5%">
                                                            <HeaderStyle Font-Bold="true" />
                                                            <PropertiesCheckEdit ValueChecked="T" ValueGrayed="N" ClientInstanceName="colIsApprove" ValueType="System.Char" ValueUnchecked="F"></PropertiesCheckEdit>
                                                        </dx:GridViewDataCheckColumn>
                                                        <dx:GridViewDataMemoColumn Name="colTypeApproval" FieldName="TypeApproval" Caption="TypeApproval" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataTextColumn Name="colDecisionState" FieldName="DecisionState" Caption="State" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn Name="colDecisionDate" FieldName="DecisionDate" Caption="Decision Date" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataTextColumn Name="colEmail" FieldName="Email" Caption="Email" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" />
                                                        </dx:GridViewDataTextColumn>
                                                        <%--<dx:GridViewDataMemoColumn Name="colDecisionNote" FieldName="DecisionNote" Caption="DecisionNote" ReadOnly="true" PropertiesMemoEdit-Height="16px">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataMemoColumn>--%>
                                                        
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        <dx:LayoutGroup Name="LayoutGroupUploadDocument" Caption="Document Upload SPD" Width="50%">
                                <Items>
                                    <dx:LayoutGroup  ShowCaption="True" Caption="Document Upload" GroupBoxDecoration="Box" ColCount="1">
                                        <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                                        <Items>
                                            <dx:LayoutItem ShowCaption="true" Caption="Document Type" ColSpan="1">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <%--<dx:ASPxComboBox runat="server" ID="cbAppType" ClientInstanceName="cbAppType" AutoPostBack="false" NullText="-- Select --">--%>
                                                        <dx:ASPxComboBox ID="documentTypeSPD" runat="server" ClientInstanceName="documentTypeSPD" NullText="-- Select --">
                                                            <Items>
                                                                <dx:ListEditItem Text="Document Surat Pengajuan Perjalanan Dinas" Value="pengajuanSPD" />
                                                                <dx:ListEditItem Text="Document Realisasi Perjalanan Dinas" Value="realisasiSPD" />
                                                                <dx:ListEditItem Text="Document Budget Perjalanan Dinas" Value="anggaranSPD" />
                                                            </Items>
                                                            <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { OnWaiveChanged(s); }" />--%>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="true" Caption="Notes" ColSpan="1">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxMemo runat="server" ID="mmNotes" ClientInstanceName="mmNotes" Width="100%" Height="100px"></dx:ASPxMemo>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="true" Caption="File" ColSpan="1">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxUploadControl runat="server" ID="UploadCtrl" ClientInstanceName="UploadCtrl" ShowProgressPanel="true" UploadMode="Auto" AutoStartUpload="true" FileUploadMode="OnPageLoad"
                                                            OnFileUploadComplete="UploadCtrl_FileUploadComplete" Width="100%">
                                                            <ValidationSettings MaxFileSize="10000000" ErrorStyle-BackColor="Red" ShowErrors="true" AllowedFileExtensions=".pdf,.jpg,.jpeg,.img,.tiff,.xls,.xlsx,.txt,.doc,.docx,.rar,.zip">
                                                                <ErrorStyle BackColor="Red"></ErrorStyle>
                                                            </ValidationSettings>
                                                            <ClientSideEvents FileUploadComplete="onUploadControlFileUploadComplete" />
                                                        </dx:ASPxUploadControl>
                                                        <dx:ASPxButton ID="btnDownload" runat="server" ClientInstanceName="btnDownload" OnClick="btnDownload_Click" Text="View Document" ClientVisible="false" Width="35%" RenderMode="Link" AutoPostBack="false" HorizontalAlign="Left" VerticalAlign="Middle">
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="true" Caption="">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton runat="server" ID="btnUpload" ClientInstanceName="btnUpload" Text="Upload" AutoPostBack="false" Width="50%">
                                                            <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('UPLOADCONFIRM;UPLOADCONFIRM;REFRESH;')}" />
                                                        </dx:ASPxButton>
                                                        <dx:ASPxLabel runat="server" Text="maximum file size is 10mb." Font-Italic="true" Font-Size="Smaller" ForeColor="Red"></dx:ASPxLabel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                        <SettingsItems VerticalAlign="Middle" />
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:LayoutGroup>
                <dx:LayoutGroup Name="LayoutGroupDocumentLibrary" Caption="Document Library SPD" Width="100%">
                    <Items>
                        <dx:LayoutGroup ShowCaption="True" Caption="Document Library" GroupBoxDecoration="Box" ColSpan="1" ColCount="1" Width="100%">
                            <GroupBoxStyle BackColor="Transparent" Caption-BackColor="#f8fafd" Caption-Font-Bold="true"></GroupBoxStyle>
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxGridView
                                                ID="gvMain"
                                                ClientInstanceName="gvMain"
                                                runat="server"
                                                KeyFieldName="ID"
                                                Width="100%"
                                                AutoGenerateColumns="False"
                                                EnableCallBacks="true"
                                                EnablePagingCallbackAnimation="true"
                                                EnableTheming="True"
                                                Theme="Glass" Font-Size="Small" Font-Names="Calibri" OnCustomButtonCallback="gvMain_CustomButtonCallback" OnDataBinding="gvMain_DataBinding">
                                                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                                </SettingsAdaptivity>
                                                <%--<ClientSideEvents
                                                EndCallback="gvMain_EndCallback"
                                                RowDblClick="function(s, e) {gvMain.PerformCallback('DOUBLECLICK;' +e.visibleIndex);}"
                                                FocusedRowChanged="FocusedRowChanged" Init="gvMain_Init" BeginCallback="gvMain_BeginCallback" 
                                                CustomButtonClick="function(s,e) { gvMain_CustomButtonClick(s,e); }"/>--%>
                                                <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                <SettingsSearchPanel Visible="True" />
                                                <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                <SettingsLoadingPanel Mode="Disabled" />
                                                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                                <SettingsPager PageSize="5"></SettingsPager>
                                                <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                                <Toolbars>
                                                    <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText" />
                                                        </Items>
                                                    </dx:GridViewToolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="colID" Caption="ID." FieldName="ID" ReadOnly="True" Visible="false">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey." FieldName="DocKey" ReadOnly="True" Visible="false">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colDocNo" Caption="DocNo" FieldName="DocumentNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colType" Caption="Type" FieldName="Type" ReadOnly="True" ShowInCustomizationForm="true" Visible="true">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colExt" Caption="Ext" FieldName="Ext" ReadOnly="True" ShowInCustomizationForm="true" Visible="false">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colNotes" Caption="Notes" FieldName="Notes" ReadOnly="True" UnboundType="String">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="CreatedBy" FieldName="CreatedBy" ReadOnly="True" UnboundType="String">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Name="colCreatedDateTime" Caption="Upload Date" FieldName="CreatedDateTime" ReadOnly="True" UnboundType="DateTime" Width="10%" Visible="True">
                                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesDateEdit>
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Link" Caption="Download">
                                                        <HeaderStyle BackColor="WhiteSmoke" Font-Bold="true" />
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="GridbtnDownload" Text=" ">
                                                                <Image Height="20px" Width="20px" Url="../../../Content/Images/download.png" ToolTip="Click here to download file."></Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Name="colCreatedBy" Caption="CreatedBy" FieldName="CreatedBy" ReadOnly="True" UnboundType="String" Width="10%" Visible="false">
                                                        <HeaderStyle Font-Bold="true" BackColor="WhiteSmoke" />
                                                    </dx:GridViewDataTextColumn>
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
                        </dx:LayoutGroup>
                    </Items>
                </dx:LayoutGroup>
            </Items>
            </dx:TabbedLayoutGroup>
            <dx:LayoutGroup Name="totalPnj" GroupBoxDecoration="Box" Caption="" ColCount="5" GroupBoxStyle-Border-BorderColor="#F8FAFD" Width="100%">
                        <Items>
                            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Total Pengajuan" CaptionStyle-Font-Bold="true">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit runat="server" ID="toPNJ" ClientInstanceName="toPNJ" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" ReadOnly="true" Font-Bold="true" ForeColor="#666666" Width="200px" Height="20px">
                                        <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('CALCULATE;'); }"/>
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
            </dx:LayoutGroup>
            <dx:EmptyLayoutItem Width="400px"></dx:EmptyLayoutItem>
            <dx:EmptyLayoutItem Width="200px"></dx:EmptyLayoutItem>

            <dx:TabbedLayoutGroup Height="200px" Name="tbLayoutRealisasi" ClientInstanceName="tbLayoutRealisasi" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0" Border-BorderColor="#d1ecee">
                        <Items>
                            <dx:LayoutGroup Name="LayoutRelisasiRincian" Caption="Rincian Realisasi Perjalanan Dinas">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="realisasiPerjalananDinas" runat="server">
                                                <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvRealisasiPerjalananDinas"
                                                    ClientInstanceName="gvRincianRealisasi"
                                                    Width="100%"
                                                    KeyFieldName="DtlKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Glass"
                                                    Font-Names="Calibri"
                                                    OnInit="gvRealisasiPerjalananDinas_Init"
                                                    OnInitNewRow="gvRealisasiPerjalananDinas_InitNewRow"
                                                    OnDataBinding="gvRealisasiPerjalananDinas_DataBinding"
                                                    OnRowInserting="gvRealisasiPerjalananDinas_RowInserting"
                                                    OnRowUpdating="gvRealisasiPerjalananDinas_RowUpdating"
                                                    OnRowDeleting="gvRealisasiPerjalananDinas_RowDeleting"
                                                    OnCustomCallback="gvRealisasiPerjalananDinas_CustomCallback"
                                                    OnAutoFilterCellEditorInitialize="gvRealisasiPerjalananDinas_AutoFilterCellEditorInitialize"
                                                    OnCustomColumnDisplayText="gvRealisasiPerjalananDinas_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents EndCallback="gvRealisasiPerjalananDinas_EndCallback" />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px">
                                                        </NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="True" Visible="false" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colTypeSPD" Caption="TypeSPD" FieldName="TypeSPD" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="5">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="typeTunjangan" Caption="Type Tunjangan" FieldName="TypeBudget" ReadOnly="false" ShowInCustomizationForm="true" VisibleIndex="6" >
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                            <PropertiesComboBox ClientInstanceName="colCondition"
                                                                TextField="Condition" ValueField="Condition" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Transportasi" Value="Transportasi" />
                                                                    <dx:ListEditItem Text="Penginapan" Value="Penginapan" />
                                                                    <dx:ListEditItem Text="Biaya Lain-Lain" Value="Biaya Lain-Lain" />
                                                                    <%--<dx:ListEditItem Text="Tunjangan Perjalanan Dinas" Value="Tunjangan Perjalanan Dinas" />--%>
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colDesc" FieldName="BudgetDesc" Caption="Description" VisibleIndex="7">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataSpinEditColumn
                                                            Name="colSubTotal"
                                                            Caption="Sub Total Perjalanan Dinas"
                                                            FieldName="BudgetAmount"
                                                            ShowInCustomizationForm="True"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}"
                                                            PropertiesSpinEdit-DisplayFormatInEditMode="true"
                                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                                            PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false"
                                                            PropertiesSpinEdit-MinValue="-9999999999999"
                                                            PropertiesSpinEdit-MaxValue="999999999999999"
                                                            VisibleIndex="8"
                                                            Width="35%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:LayoutGroup Name="lgDetailApprovalRealisasi" Caption="Approval Realisasi Perjalanan Dinas" GroupBoxStyle-Caption-BackColor="White" ColCount="4" Visible="True">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    runat="server"
                                                    ID="gvApprovalRealisasi"
                                                    ClientInstanceName="gvApprovalRealisasi"
                                                    Width="100%"
                                                    KeyFieldName="DtlKey"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Aqua"
                                                    Font-Names="Calibri"
                                                    OnDataBinding="gvApprovalRealisasi_DataBinding"
                                                    OnRowInserting="gvApprovalRealisasi_RowInserting"
                                                    OnRowUpdating="gvApprovalRealisasi_RowUpdating"
                                                    OnRowDeleting="gvApprovalRealisasi_RowDeleting"
                                                    OnCustomCallback="gvApprovalRealisasi_CustomCallback"
                                                    OnAutoFilterCellEditorInitialize="gvApprovalRealisasi_AutoFilterCellEditorInitialize" OnCellEditorInitialize="gvApprovalRealisasi_CellEditorInitialize"
                                                    OnCustomColumnDisplayText="gvApprovalRealisasi_CustomColumnDisplayText">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents EndCallback="gvApprovalRealisasi_EndCallback" />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px">
                                                        </NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" VisibleIndex="0" Width="2%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlAppKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="True" Visible="false" VisibleIndex="2">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colNIK" FieldName="NIK" ShowInCustomizationForm="True" Caption="NIK" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colNama" FieldName="Nama" ShowInCustomizationForm="True" Caption="Nama" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                            <PropertiesComboBox EnableCallbackMode="true" ClientInstanceName="colNama" DropDownRows="10" IncrementalFilteringDelay="500" IncrementalFilteringMode="Contains" DisplayFormatString="{1}" TextFormatString="{1}" DropDownStyle="DropDownList" ValueField="Nama" TextField="Nama" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e) {OnNameChangedRel(s);}" />
                                                                <ItemStyle Wrap="True"></ItemStyle>
                                                                <Columns>
                                                                    <dx:ListBoxColumn FieldName="NIK" Caption="NIK" Width="150px" />
                                                                    <dx:ListBoxColumn FieldName="Nama" Caption="Nama" Width="150px" />
                                                                    <dx:ListBoxColumn FieldName="Email" Caption="Email" Width="150px"/>
                                                                </Columns>
                                                                <ValidationSettings ValidateOnLeave="true" ValidationGroup="ValidationSave" Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                                    <RequiredField ErrorText="* Value can't be empty." IsRequired="true" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colJabatan" FieldName="Jabatan" Caption="Jabatan" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataCheckColumn Name="colIsDecision" FieldName="IsDecision" ShowInCustomizationForm="True" Caption="Decision?" ReadOnly="true" Width="5%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                            <PropertiesCheckEdit ValueChecked="T" ValueGrayed="N" ClientInstanceName="colIsApprove" ValueType="System.Char" ValueUnchecked="F"></PropertiesCheckEdit>
                                                        </dx:GridViewDataCheckColumn>
                                                        <dx:GridViewDataMemoColumn Name="colTypeApproval" FieldName="TypeApproval" Caption="TypeApproval" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataTextColumn Name="colDecisionState" FieldName="DecisionState" Caption="State" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn Name="colDecisionDate" FieldName="DecisionDate" Caption="Decision Date" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataTextColumn Name="colEmail" FieldName="Email" Caption="Email" ReadOnly="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataTextColumn>
                                                        <%--<dx:GridViewDataMemoColumn Name="colDecisionNote" FieldName="DecisionNote" Caption="DecisionNote" ReadOnly="true" PropertiesMemoEdit-Height="16px">
                                                            <HeaderStyle Font-Bold="true" BackColor="#f0f0f0" ForeColor="#1872c4" />
                                                        </dx:GridViewDataMemoColumn>--%>
                                                        
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
            </dx:TabbedLayoutGroup>
            <dx:LayoutGroup Name="totalRel" GroupBoxDecoration="Box" Caption="" ColCount="5" GroupBoxStyle-Border-BorderColor="#F8FAFD" Width="100%">
                                <Items>
                                    <dx:EmptyLayoutItem Width="71%"></dx:EmptyLayoutItem>
                                    <dx:LayoutItem Caption="Total Realisasi" CaptionStyle-Font-Bold="true">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxSpinEdit runat="server" ID="toREL" ClientInstanceName="toREL" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" ReadOnly="true" Font-Bold="true" ForeColor="#666666" Width="200px" Height="20px">
                                                    <ClientSideEvents ValueChanged="function(s,e) { cplMain.PerformCallback('CALCULATE_REALISASI;'); }" />
                                                </dx:ASPxSpinEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                            <dx:EmptyLayoutItem Width="400px"></dx:EmptyLayoutItem>
                            <dx:EmptyLayoutItem Width="200px"></dx:EmptyLayoutItem>
            <dx:EmptyLayoutItem Name="spaceBtn" Width="40%"></dx:EmptyLayoutItem>
            <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnApprove" ClientInstanceName="btnApprove" Text="Approve" ForeColor="Green" AutoPostBack="false" Theme="Glass">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('APPROVE_CONFIRM;' + 'APPROVE_CONFIRM;'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Text="Reject" ForeColor="Red" AutoPostBack="false" Theme="Glass">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REJECT_CONFIRM;' + 'REJECT_CONFIRM;'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Name="btnSubmit" ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnSubmit" ClientInstanceName="btnSubmit" Text="Submit" AutoPostBack="false" Theme="Glass" ClientVisible="false">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SUBMIT_CONFIRM;' + 'SUBMIT_CONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Name="btnSave" ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnSave" ClientInstanceName="btnSave" Text="Save" AutoPostBack="false" Theme="Glass" UseSubmitBehavior="false" ValidationGroup="ValidationSave">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SAVE_CONFIRM;' + 'SAVE_CONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="false" Caption="" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" AutoPostBack="true" Theme="Glass" PostBackUrl="~/Transactions/Application/PerjalananDinas/PerjalananDinasList.aspx"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
     </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" BackgroundImage-HorizontalPosition="center" Theme="Glass" Text="Loading Please Wait...">
        <BackgroundImage VerticalPosition="top" />
    </dx:ASPxLoadingPanel>
</asp:Content>