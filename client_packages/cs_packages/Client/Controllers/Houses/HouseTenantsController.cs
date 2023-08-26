using Client.Managers;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;

namespace Client.Controllers.Houses
{
    public class HouseTenantsController
    {
        private List<string> _tenants_list = null;
        private HtmlWindow _window_phone = new HtmlWindow("package://CEF/phone/index.html");
        private bool _is_phone_open = false;

        public HouseTenantsController() 
        {
            Events.Add("CEF:CLIENT::ON_INIT_TENANTS_LIST", OnPlayerEnterTenantsList);
            Events.Add("SERVER:CLIENT::SEND_TENANT_LIST", OnSendTenantList);
            Events.Add("CEF:CLIENT::ON_CLICK_BUTTON_DELETE_TENANT", OnClickButtonDeleteTenant);
            RAGE.Events.Tick += OnClientOpenPhone;

            _window_phone.Active = false;
        }

        private void OnClientOpenPhone(List<Events.TickNametagData> nametags)
        {
            KeyManager.KeyBind(KeyManager.KeyUp, () =>
            {
                _is_phone_open = !_is_phone_open;
                _window_phone.Active = _is_phone_open;

                Cursor.ShowCursor(_is_phone_open, _is_phone_open);
            });
        }

        private void OnSendTenantList(object[] args)
        {
            //Chat.Output(_tenants_list.Count.ToString() + "sssssssssssss");
            string tenant_item = (string)args[0];

            Chat.Output(tenant_item);

            _window_phone.Call("CLIENT:CEF::INIT_TENANT_LIST", tenant_item);
        }

        private void OnPlayerEnterTenantsList(object[] args)
        {
            Events.CallRemote("CLIENT:SERVER::INITIALIZE_TENANTS_LIST");
        }

        private void OnClickButtonDeleteTenant(object[] args)
        {
            Events.CallRemote("CLIENT:SERVER::DELETE_TENANT", (string)args[0]);
        }
    }
}
