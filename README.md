new

## Security settings for legacy web services

The Warehouse Web Site reads the following secrets from protected deployment configuration (`appSettings`); no production values belong in source control:

| Setting | Used by |
| --- | --- |
| `WarehouseApiKey` | `ExportDataMPWLC_MPSCSC` mutations, NAFED/NCCF NeML transaction imports, and Android read methods. Send it in the `X-Warehouse-Api-Key` HTTP header. |
| `WarehouseDscApiCredential` | `Upload_DSC.Insert_DSC_Data`, supplied through its existing `Credential` SOAP argument. |
| `MPSCSCStorageBillApiCredential` | `Get_MPSCSC_StorageBill_List`, supplied through its existing `Credential` SOAP argument. |
| `NAFEDNEMLServiceUsername`, `NAFEDNEMLServicePassword` | Credentials accepted by `SendDataToNEML.GetProcedureData`. |
| `NAFEDNEMLApiUsername`, `NAFEDNEMLApiPassword` | Outbound NAFED NeML API login. |
| `ESamyuktiServiceUsername`, `ESamyuktiServicePassword` | Credentials accepted by `SendDataToNEML_eSamyukti.GetProcedureData`. |
| `NCCFNeMLApiUsername`, `NCCFNeMLApiPassword` | Outbound NCCF NeML API login. |
| `FciCfspApiCredential` | Credential for `WebService_for_FCI` methods. |
| `NafedWsUsername`, `NafedWsPassword` | Credentials for the NAFED integration service. |
| `FciMssUsername`, `FciMssPassword` | Credentials for `FCIAPIWebService`. |
| `FciCfspUsername`, `FciCfspPassword` | Credentials for `FCI_CFSP_WS`. |
| `FciCfspExternalUsername`, `FciCfspExternalPassword` | Outbound login for the CFSP token operation. |
| `GodownSurveyorUsername`, `GodownSurveyorPassword2020`, `GodownSurveyorPassword2019` | Credentials for the Godown Surveyor service's versioned operations. |
| `LegacyWlcBillCredential` | Legacy WLC credential shared by NAFED bill and DSC signing/retrieval methods. |
| `LegacyWlcWhrCredential` | Legacy WLC credential for Rabi WHR data methods. |
| `LegacyWlcWmsCredential` | Legacy WLC credential for the WMS godown service. |
| `CcrlServiceUsername`, `CcrlServicePassword` | Credentials accepted by `SendDataToCCRL`. |
| `EncryptPasswordMasterSecret` | Server-side legacy master-password hash generation in `EncryptPassword.aspx`. Keep the deployed value stable to preserve existing database hashes. |
| `EncryptPasswordDefaultPasswordSuffix` | Server-side generation of the legacy default password values displayed by `EncryptPassword.aspx`. |

Provision these settings through the hosting environment or an encrypted .NET Framework configuration section. Rotate/reissue every credential moved out of source; removing a literal does not revoke copies in repository history. Configure service callers to send the API-key header where required; SOAP method signatures remain unchanged. `EncryptPassword.aspx` still uses MD5 because the existing database contract requires it; the secret is no longer sent to the browser, but the legacy hash scheme should be replaced as part of a separately planned authentication migration.
