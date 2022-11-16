﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Neos.IdentityServer.MultiFactor.Common;
using Neos.IdentityServer.MultiFactor;

namespace Neos.IdentityServer.Multifactor.SMS.Test
{
    public class QuizProviderSample : BaseExternalProvider
    {
        private bool _isinitialized = false;
        private bool IsAsync;

        /// <summary>
        /// Kind property implementation
        /// 
        /// For custom implementation it' required that you return PreferredMethod.External 
        /// </summary>
        public override PreferredMethod Kind
        {
            get { return PreferredMethod.External; }
        }

        /// <summary>
        /// IsBuiltIn property implementation
        /// </summary>
        public override bool IsBuiltIn
        {
            get { return false; }
        }

        /// <summary>
        /// CanBeDisabled property implementation
        /// 
        /// Allow the provider to be disabled in the UI, and not available to the users
        /// 
        /// </summary>
        public override bool AllowDisable
        {
            get { return true; }
        }

        /// <summary>
        /// IsInitialized property implmentation
        /// </summary>
        public override bool IsInitialized
        {
            get { return _isinitialized; }
        }

        /// <summary>
        /// CanOverrideDefault property implmentation
        /// 
        /// When the provider propose multiple options (aka : sms, voice, otp), let the user to change the sub method at login
        /// </summary>
        public override bool AllowOverride
        {
            get { return true; }
        }

        /// <summary>
        /// AllowEnrollment property implementation
        /// </summary>
        public override bool AllowEnrollment
        {
            get { return true; }
        }

        /// <summary>
        /// LockUserOnDefaultProvider property implementation
        /// </summary>
        public override bool LockUserOnDefaultProvider { get; set; } = false;

        /// <summary>
        /// ForceEnrollment property implementation
        /// </summary>
        public override ForceWizardMode ForceEnrollment
        {
            get { return ForceWizardMode.Disabled; }
            set { }
        }

        /// <summary>
        /// Name property implementation
        /// </summary>
        public override string Name
        {
            get { return "Neos.Provider.Quiz"; }
        }

        /// <summary>
        /// Description property implementation
        /// </summary>
        public override string Description
        {
            get
            {
                int lcid = 0;
                if (CultureInfo.DefaultThreadCurrentUICulture != null)
                    lcid = CultureInfo.DefaultThreadCurrentUICulture.LCID;
                else
                    lcid = CultureInfo.CurrentUICulture.LCID;

                switch (lcid)
                {
                    default:
                        return "Quiz Multi-Factor Provider Sample";
                    case 1036:
                        return "Fournisseur multifacteur Quiz";
                    case 1034:
                    case 3082:
                        return "Cuestionario multifactorial";
                }
            }
        }

        /// <summary>
        /// GetUILabel method implementation
        /// Label displayed above a control (aka : TextBox) when user is in login process
        /// </summary>
        public override string GetUILabel(AuthenticationContext ctx)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            GetAuthenticationContext(ctx);
            switch (ctx.SelectedMethod)
            {
                case AuthenticationResponseKind.Sample1:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Age of a celebrity";
                        case 1036:
                            return "Age d'une personne célébre";
                        case 1034:
                        case 3082:
                            return "La edad de una persona famosa";
                    }
                case AuthenticationResponseKind.Sample2Async:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Sing a song";
                        case 1036:
                            return "Chanter une chanson";
                        case 1034:
                        case 3082:
                            return "Cantar una canción";
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// GetWizardUIComment method implementation
        /// </summary>
        public override string GetWizardUIComment(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Follow the instructions to authenticate";
                case 1036:
                    return "Suivez les indications pour vous authentifier";
                case 1034:
                case 3082:
                    return "Sigue las instrucciones para autenticarte";
            }
        }

        /// <summary>
        /// GetWizardUILabel method implementation
        /// </summary>
        public override string GetWizardUILabel(AuthenticationContext ctx)
        {
            return GetWizardLinkLabel(ctx);
        }

        /// <summary>
        /// GetWizardLinkLabel method implementation
        /// </summary>
        public override string GetWizardLinkLabel(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Register for the contest";
                case 1036:
                    return "Enregistrer vous au concours";
                case 1034:
                case 3082:
                    return "Registrarse para el concurso";
            }
        }

        /// <summary>
        /// GetUICFGLabel method implementation
        /// </summary>
        public override string GetUICFGLabel(AuthenticationContext ctx)
        {
            return GetUIListOptionLabel(ctx);
        }

        /// <summary>
        /// GetUIMessage method implmentation
        /// 
        /// Label displayed under a control (aka : TextBox) as help message when user is in login process
        /// </summary>
        public override string GetUIMessage(AuthenticationContext ctx)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            GetAuthenticationContext(ctx);
            switch (ctx.SelectedMethod)
            {
                case AuthenticationResponseKind.Sample1:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Indicate Barack Obama's age";
                        case 1036:
                            return "Indiquer l'âge de Paul Bismuth";
                        case 1034:
                        case 3082:
                            return "La edad de una persona famosa";
                    }
                case AuthenticationResponseKind.Sample2Async:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Sing Queen's \"Don't stop me now !\"";
                        case 1036:
                            return "Chanter \"Nougayork\" de Claude Nougaro";
                        case 1034:
                        case 3082:
                            return "Canta \"Waka Waka\" de Shakira";
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// GetUIListOptionLabel method implementation
        /// </summary>
        public override string GetUIListOptionLabel(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Take part in a Quiz";
                case 1036:
                    return "Participer à un Quiz";
                case 1034:
                case 3082:
                    return "Participa en un cuestionario";
            }
        }

        /// <summary>
        /// GetUIListChoiceLabel method implmentation
        /// </summary>
        public override string GetUIListChoiceLabel(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Take part in a Quiz";
                case 1036:
                    return "Participer à un Quiz";
                case 1034:
                case 3082:
                    return "Participa en un cuestionario";
            }
        }

        /// <summary>
        /// GetUIDefaultChoiceLabel method implementation
        /// </summary>
        public override string GetUIDefaultChoiceLabel(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Use Default option";
                case 1036:
                    return "Utiliser l'option par défaut";
                case 1034:
                case 3082:
                    return "Usar la opción predeterminada";
            }
        }

        /// <summary>
        /// GetUIConfigLabel method implmentation
        /// 
        /// Label displayed in the comboxbox used in configuration (registration)
        /// </summary>
        public override string GetUIConfigLabel(AuthenticationContext ctx)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            GetAuthenticationContext(ctx);
            switch (ctx.SelectedMethod)
            {
                case AuthenticationResponseKind.Sample1:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Barack Obama's age";
                        case 1036:
                            return "Age de Paul Bismuth";
                        case 1034:
                        case 3082:
                            return "La edad de Raphaël Nadal";
                    }
                case AuthenticationResponseKind.Sample2Async:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Don't stop me now !";
                        case 1036:
                            return "Nougayork";
                        case 1034:
                        case 3082:
                            return "Waka Waka";
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// GetUIChoiceLabel method ipmplmentation
        /// 
        /// Label displayed in the comboxbox used for selection of the Default Authentication method (registration, Do not have the code)
        /// </summary>
        public override string GetUIChoiceLabel(AuthenticationContext ctx, AvailableAuthenticationMethod method = null)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            GetAuthenticationContext(ctx);
            AuthenticationResponseKind mk = ctx.SelectedMethod;
            if (method != null)
                mk = method.Method;
            switch (mk)
            {
                case AuthenticationResponseKind.Sample1:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "How old is Barak Obama";
                        case 1036:
                            return "Quel est l'age de Paul Bismuth";
                        case 1034:
                        case 3082:
                            return "¿ Qué edad tiene Raphael Nadal";
                    }
                case AuthenticationResponseKind.Sample2Async:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Sing Queen's \"Don't stop me now !\"";
                        case 1036:
                            return "Chanter \"Nougayork\" de Claude Nougaro";
                        case 1034:
                        case 3082:
                            return "Canta \"Waka Waka\" de Shakira";
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// GetUIWarningInternetLabel method implmentation
        /// 
        /// Warning message (optional) when data is sent over the internet
        /// </summary>
        public override string GetUIWarningInternetLabel(AuthenticationContext ctx)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            GetAuthenticationContext(ctx);
            switch (ctx.SelectedMethod)
            {
                case AuthenticationResponseKind.Sample1:
                    return string.Empty;
                case AuthenticationResponseKind.Sample2Async:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "Tonight I'm gonna have myself a real good time<BR>" +
                                   "I feel alive and the world I'll turn it inside out - yeah<BR>" +
                                   "And floating around in ecstasy<BR>" +
                                   "So don't stop me now don't stop me<BR>" +
                                   "'Cause I'm having a good time having a good time<BR>";
                        case 1036:
                            return "Dès l'aérogare J'ai senti le choc<BR>" +
                                   "Un souffle barbare, Un remous hard rock<BR>" +
                                   "Dès l'aérogare J'ai changé d'époque<BR>" +
                                   "Come on ! ça démarre sur les starting blocks<BR>" +
                                   "Dare, Dare, Dare ! là c'est du mastoque<BR>" +
                                   "C'est pas du Ronsard, c'est de l'amerloque...<BR>" +
                                   "(feat Marcus Miller on the bass)<BR>";
                        case 1034:
                        case 3082:
                            return "You're a good soldier Choosing your battles<BR>" +
                                   "Pick yourself up and dust yourself off and back in the saddle<BR>" +
                                   "You're on the front line Everyone's watching<BR>" +
                                   "You know it's serious we're getting closer, this isn't over<BR>";
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// GetUIWarningThirdPartyLabel method implementation
        /// 
        /// Warning mesage (optional) when the MFA validation is made by a third party
        /// </summary>
        public override string GetUIWarningThirdPartyLabel(AuthenticationContext ctx)
        {

            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            GetAuthenticationContext(ctx);
            switch (ctx.SelectedMethod)
            {
                case AuthenticationResponseKind.Sample1:
                    return string.Empty;
                case AuthenticationResponseKind.Sample2Async:
                    switch (ctx.Lcid)
                    {
                        default:
                        case 1033:
                            return "(Sing for 30 seconds)";

                        case 1036:
                            return "(Chantez durant 30 secondes)";
                        case 1034:
                        case 3082:
                            return "(Canta durante 30 segundos)";
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// IsAvailable method implmentation
        /// </summary>
        public override bool IsAvailable(AuthenticationContext ctx)
        {
            if (LockUserOnDefaultProvider)
            {
                if (ctx.PreferredMethod != this.Kind)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// IsAvailableForUser method implmentation
        /// </summary>
        public override bool IsAvailableForUser(AuthenticationContext ctx)
        {
            if (LockUserOnDefaultProvider)
            {
                if (ctx.PreferredMethod != this.Kind)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// GetUIEnrollmentTaskLabel method implementation
        /// </summary>
        public override string GetUIEnrollmentTaskLabel(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Register for a Quiz";
                case 1036:
                    return "S'enregister pour un Quiz";
                case 1034:
                case 3082:
                    return "Registrarse para un cuestionario";
            }
        }

        /// <summary>
        /// GetUIEnrollValidatedLabel method implementation
        /// </summary>
        public override string GetUIEnrollValidatedLabel(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Account verified<br><br>Now you can use the Quiz to login";
                case 1036:
                    return "Compte validé<br><br>Vous pouvez utiliser les Quiz pour vous connecter";
                case 1034:
                case 3082:
                    return "Cuenta verificada<br><br>Ahora puede usar el Quiz para iniciar sesión";
            }
        }

        /// <summary>
        /// GetUIAccountManagementLabel method implementation
        /// </summary>
        public override string GetUIAccountManagementLabel(AuthenticationContext ctx)
        {
            switch (ctx.Lcid)
            {
                default:
                    return "Access my Quiz configuration options";
                case 1036:
                    return "Accéder à mes options de configuration Quiz";
                case 1034:
                case 3082:
                    return "Acceda a mis opciones de configuración Quiz";
            }
        }

        /// <summary>
        /// GetAccountManagementUrl() method implmentation
        /// </summary>
        public override string GetAccountManagementUrl(AuthenticationContext ctx)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            GetAuthenticationContext(ctx);

            switch (ctx.SelectedMethod)
            {
                case AuthenticationResponseKind.Sample1:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "https://en.wikipedia.org/wiki/Barack_Obama";
                        case 1036:
                            return "https://fr.wikipedia.org/wiki/Nicolas_Sarkozy";
                        case 1034:
                        case 3082:
                            return "https://es.wikipedia.org/wiki/Rafael_Nadal";
                    }
                case AuthenticationResponseKind.Sample2Async:
                    switch (ctx.Lcid)
                    {
                        default:
                            return "https://www.shazam.com/track/219983/dont-stop-me-now";
                        case 1036:
                            return "https://www.shazam.com/fr/track/45655670/nougayork";
                        case 1034:
                        case 3082:
                            return "https://www.shazam.com/es/track/52114610/waka-waka-this-time-for-africa";
                    }
            }
            return string.Empty;
        }

        /// <summary>
        /// IsMethodElementRequired implementation
        /// </summary>
        public override bool IsUIElementRequired(AuthenticationContext ctx, RequiredMethodElements element)
        {
            switch (element)
            {
                case RequiredMethodElements.CodeInputRequired:
                    return (ctx.SelectedMethod == AuthenticationResponseKind.Sample1);
                // case RequiredMethodElements.ExternalParameterRequired:
                //     return (ctx.SelectedMethod == AuthenticationResponseKind.Sample1);
                case RequiredMethodElements.ExternalLinkRaquired:
                    return true;
                case RequiredMethodElements.PinInputRequired:
                    return this.PinRequired;
                case RequiredMethodElements.PinParameterRequired:
                    return this.PinRequired;
                case RequiredMethodElements.PinLinkRequired:
                    return this.PinRequired;
            }
            return false;
        }

        /// <summary>
        /// Initialize method implementation
        /// </summary>
        public override void Initialize(BaseProviderParams externalsystem)
        {
            try
            {
                if (!_isinitialized)
                {
                    if (externalsystem is ExternalProviderParams)
                    {
                        ExternalProviderParams param = externalsystem as ExternalProviderParams;
                        Enabled = param.Enabled;
                        PinRequired = param.PinRequired;
                        IsRequired = param.IsRequired;
                        WizardEnabled = param.EnrollWizard;
                        WizardDisabled = param.EnrollWizardDisabled;
                        ForceEnrollment = param.ForceWizard;
                        IsAsync = param.Data.IsTwoWay;
                        _isinitialized = true;
                        return;
                    }
                    else
                    {
                        Enabled = externalsystem.Enabled;
                        PinRequired = externalsystem.PinRequired;
                        WizardEnabled = externalsystem.EnrollWizard;
                        ForceEnrollment = externalsystem.ForceWizard;
                        _isinitialized = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Enabled = false;
                throw ex;
            }
        }

        /// <summary>
        /// GetAuthenticationMethods method implmentation
        /// </summary>
        public override List<AvailableAuthenticationMethod> GetAuthenticationMethods(AuthenticationContext ctx)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            List<AvailableAuthenticationMethod> result = GetSessionData(this.Kind, ctx);
            if (result != null)
                return result;
            else
            {
                result = new List<AvailableAuthenticationMethod>();

                AvailableAuthenticationMethod item1 = GetAuthenticationMethodProperties(ctx, AuthenticationResponseKind.Sample1);
                result.Add(item1);
                AvailableAuthenticationMethod item2 = GetAuthenticationMethodProperties(ctx, AuthenticationResponseKind.Sample2Async);
                result.Add(item2);
                SaveSessionData(this.Kind, ctx, result);
            }
            return result;
        }

        /// <summary>
        /// PostAutnenticationRequest method implmentation
        /// </summary>
        public override int PostAuthenticationRequest(AuthenticationContext ctx)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            if (ctx.SelectedMethod == AuthenticationResponseKind.Error)
                GetAuthenticationContext(ctx);

            ctx.Notification = (int)ctx.SelectedMethod;
            ctx.SessionId = Guid.NewGuid().ToString();
            ctx.SessionDate = DateTime.Now;
            return (int)ctx.SelectedMethod;
        }

        /// <summary>
        /// SetAuthenticationResult method implmentation
        /// </summary>
        public override int SetAuthenticationResult(AuthenticationContext ctx, string pin)
        {
            if (!IsInitialized)
                throw new Exception("Provider not initialized !");

            if (CheckPin(ctx, pin))
                return (int)AuthenticationResponseKind.PhoneAppOTP;
            else
                return (int)AuthenticationResponseKind.Error;
        }

        /// <summary>
        /// GetAuthenticationMethod method implementation
        /// </summary>
        private AvailableAuthenticationMethod GetAuthenticationMethodProperties(AuthenticationContext ctx, AuthenticationResponseKind method)
        {
            AvailableAuthenticationMethod result = new AvailableAuthenticationMethod();
            switch (method)
            {
                case AuthenticationResponseKind.Sample1:
                    result.IsDefault = !IsAsync;
                    result.RequiredPin = false;
                    result.IsRemote = false;
                    result.IsTwoWay = false;
                    result.IsSendBack = false;
                    result.RequiredEmail = false;
                    result.RequiredPhone = false;
                    result.RequiredCode = true;
                    result.Method = MultiFactor.AuthenticationResponseKind.Sample1;
                    break;
                case AuthenticationResponseKind.Sample2Async:
                    result.IsDefault = IsAsync;
                    result.RequiredPin = false;
                    result.IsRemote = true;
                    result.IsTwoWay = true;
                    result.IsSendBack = false;
                    result.RequiredEmail = false;
                    result.RequiredPhone = false;
                    result.RequiredCode = false;
                    result.Method = MultiFactor.AuthenticationResponseKind.Sample2Async;
                    break;
                default:
                    result.Method = AuthenticationResponseKind.Error;
                    break;
            }
            return result;
        }

        /// <summary>
        /// CheckPin method inplementation
        /// </summary>
        private bool CheckPin(AuthenticationContext ctx, string pin)
        {
            switch (ctx.SelectedMethod)
            {
                case AuthenticationResponseKind.Sample1:
                    switch (ctx.Lcid)
                    {
                        case 1033:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1961, 08, 04), DateTime.Now));
                        case 1036:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1955, 01, 22), DateTime.Now));
                        case 1034:
                        case 3082:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1986, 06, 03), DateTime.Now));
                        default:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1961, 08, 04), DateTime.Now));
                    }
                case AuthenticationResponseKind.Sample2Async:
                    Thread.Sleep(30 * 1000);
                    return true;
                default:
                    switch (ctx.Lcid)
                    {
                        case 1033:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1961, 08, 04), DateTime.Now));
                        case 1036:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1955, 01, 22), DateTime.Now));
                        case 1034:
                        case 3082:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1986, 06, 03), DateTime.Now));
                        default:
                            return (Convert.ToInt32(pin) == GetAgeInYears(new DateTime(1961, 08, 04), DateTime.Now));
                    }
            }
        }

        /// <summary>
        /// GetAgeInYears method 
        /// </summary>
        private int GetAgeInYears(DateTime startDate, DateTime endDate)
        {
            int years = endDate.Year - startDate.Year;
            if (startDate.Month == endDate.Month && endDate.Day < startDate.Day || endDate.Month < startDate.Month)
                years--;
            return years;
        }
    }

    /// <summary>
    /// Old ExternalProvider API Sample
    /// </summary>
    public class ExternalLegacySample : IExternalOTPProvider
    {
        /// <summary>
        /// GetUserCodeWithExternalSystem demo method
        /// </summary>
#pragma warning disable 162
        public int GetUserCodeWithExternalSystem(string upn, string phonenumber, string email, ExternalOTPProvider externalsys, CultureInfo culture)
        {
            // Compute and send your TOTP code and return his value if everything goes right
            if (true)
                return 1230;
            else
                return (int)AuthenticationResponseKind.Error;  // return error
        }

        /// <summary>
        /// GetCodeWithExternalSystem method implementation for Azure MFA 
        /// </summary>
        public AuthenticationResponseKind GetCodeWithExternalSystem(MFAUser reg, ExternalOTPProvider externalsys, CultureInfo culture, out int otp)
        {
            // Compute and send your TOTP code and return his value if everything goes right
            if (true)
            {
                otp = 1230;
                return AuthenticationResponseKind.SmsOTP;
            }
            else
                return AuthenticationResponseKind.Error;  // return error
        }
#pragma warning restore 162
    }
}
