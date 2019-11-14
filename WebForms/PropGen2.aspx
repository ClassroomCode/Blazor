<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropGen2.aspx.cs" Inherits="WebForms.PropGen2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Accelebrate PropGen</title>
  <link href="master.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
      <p align="center" class="bigger"><strong>Accelebrate Proposal Generator</strong></p>
      <p align="center"><a href="/proposals">< Back to Proposal Home</a></p>
      <table border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td align="center">
            <form action="/proposals/propgen" method="post">
              <div>
                <table style="width: 100%; border-color: #b5c7de" border="1" cellpadding="0" cellspacing="0">
                  <tr>
                    <td style="font-weight: bold; color: white; font-family: Verdana; font-size: small;" bgcolor="#507cd1" align="center">Proposal Information</td>
                  </tr>
                  <tr>
                    <td align="center" bgcolor="#eff3fb">
                      <table style="font-size: 10pt; font-family: Verdana; width: 100%">
                        <tr>
                          <td align="right" style="width: 147px">Client Name:</td>
                          <td align="left">
                            <input data-val="true" data-val-required="Name Required" id="ClientName" name="ClientName" style="width: 260px" type="text" value="" />&nbsp;&nbsp;<span class="field-validation-valid" data-valmsg-for="ClientName" data-valmsg-replace="true"></span></td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">Proposal Type:</td>
                          <td align="left">
                            <input id="ProposalTypeOnsite" type="radio" name="ProposalType" value="Onsite" /><label for="ProposalTypeOnsite">On-site</label>
                            <input id="ProposalTypeOnline" type="radio" name="ProposalType" value="Online" /><label for="ProposalTypeOnline">Online</label>
                            <input id="ProposalTypeHybrid" type="radio" name="ProposalType" value="Hybrid" checked /><label for="ProposalTypeHybrid">Both</label>
                          </td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">Training Location:</td>
                          <td align="left">
                            <input id="TrainingLocation" name="TrainingLocation" style="width: 260px" type="text" value="" />&nbsp;&nbsp;<span class="field-validation-valid" data-valmsg-for="TrainingLocation" data-valmsg-replace="true"></span></td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">Custom Course Title:</td>
                          <td align="left">
                            <input id="CustomCourseTitle" name="CustomCourseTitle" style="width: 260px" type="text" value="" /></td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">High Cost Market:
                          </td>
                          <td align="left">
                            <input data-val="true" data-val-required="The HighCostMarket field is required." id="HighCostMarket" name="HighCostMarket" type="checkbox" value="true" /><input name="HighCostMarket" type="hidden" value="false" />
                          </td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">Sales Tax Required:
                          </td>
                          <td align="left">
                            <input data-val="true" data-val-required="The SalesTaxRequired field is required." id="SalesTaxRequired" name="SalesTaxRequired" type="checkbox" value="true" /><input name="SalesTaxRequired" type="hidden" value="false" />
                            (NM or CT)
                          </td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="" style="width: 147px">Airfare:
                          </td>
                          <td align="left">
                            <input id="AirfareStandard" type="radio" name="Airfare" value="Standard" checked="&quot;checked&quot;" /><label for="AirfareStandard">Included</label>
                            <input id="AirfarePremium" type="radio" name="Airfare" value="Premium" /><label for="AirfarePremium">Premium (+$2000)</label>
                            <input id="AirfareUltraPremium" type="radio" name="Airfare" value="UltraPremium" /><label for="AirfareUltraPremium">Ultra Premium (+$3000)</label>
                          </td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">Valid for:</td>
                          <td align="left">
                            <input data-val="true" data-val-number="The field ValidDays must be a number." data-val-required="The ValidDays field is required." id="ValidDays" name="ValidDays" style="width: 25px" type="text" value="60" />
                            days
                          </td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">Country:
                          </td>
                          <td align="left">
                            <select name="ClientCountry" id="ClientCountry">
                              <option value="Australia">Australia</option>
                              <option value="Canada">Canada</option>
                              <option value="Denmark">Denmark</option>
                              <option value="Finland">Finland</option>
                              <option value="Ireland">Ireland</option>
                              <option value="Japan">Japan</option>
                              <option value="Netherlands">Netherlands</option>
                              <option value="NewZealand">New Zealand</option>
                              <option value="Norway">Norway</option>
                              <option value="OtherEU">Other EU</option>
                              <option value="Singapore">Singapore</option>
                              <option value="SouthAfrica">South Africa</option>
                              <option value="Sweden">Sweden</option>
                              <option value="Switzerland">Switzerland</option>
                              <option value="UnitedKingdom">United Kingdom</option>
                              <option selected="selected" value="UnitedStates">United States</option>
                              <option value="Other">[Other]</option>
                            </select>
                            <input id="InfoRequestCountry" name="InfoRequestCountry" type="hidden" value="" />
                          </td>
                        </tr>
                        <tr>
                          <td align="right" nowrap="nowrap" style="width: 147px">Currency:
                          </td>
                          <td align="left">
                            <div>
                              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                  <asp:DropDownList ID="CurrencyList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CurrencyList_SelectedIndexChanged"></asp:DropDownList>
                                  &nbsp;<asp:Label ID="CurrencyRateLabel" runat="server" Text=""></asp:Label>
                                </ContentTemplate>
                              </asp:UpdatePanel>                  
                            </div>
                          </td>
                        </tr>
                      </table>
                      <input type="hidden" name="CurrencyRate" id="CurrencyRate" value="1" />
                    </td>
                  </tr>
                </table>
                <br />
              </div>
              <br />
              <input type="submit" name="btnGenerate" value="Generate Proposal" id="btnGenerate" />
              <br />
              <br />
            </form>
            <p style="color: red"><strong></strong></p>
          </td>
        </tr>
      </table>
      <p>
        &nbsp;
      </p>
    </div>
  </form>
</body>
</html>
