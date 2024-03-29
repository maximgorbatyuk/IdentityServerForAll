# IdentityServerForAll

## How to start apps

0. Install Docker, .netcore3.1 sdk, .net6 sdk
2. Open the /src/IdentityServer.sln with Visual Studio or Rider
3. Set up multiple project launch with your IDE. You should start all 2 apps: IdentityServer and WebApi

## How to setup IS4 in your project (ru)

### Настройка IS4

1. Добавить себе в солюшн проект [IS4](https://github.com/maximgorbatyuk/IdentityServerForAll/tree/master/src/IdentityServer)
2. Настроить ресурс api в конфиге [IS4](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/IdentityConfig.cs#L25)
  - core.api - это идентификатор скоупа. Здесь может быть название модуля либо добенной области проекта. Если деления на разные API нет, то допустимо оставить любой идентификатор
3. Добавить клиентов - SPA, m2m, etc - [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/IdentityConfig.cs#L30)
  - В качестве секрета [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/Clients/M2MSimpleClient.cs#L11), [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/Clients/InteractiveMvcClient.cs#L12) и [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/Clients/WebBrowserStubClient.cs#L14) можно использовать любую строку. Ее же нужно будет упомянуть в настройках самого клиента.
  - Не забыть указать [идентификатор](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/IdentityConfig.cs#L59) скоупа API, а также другие необходимые скоупы
  - Не забыть указать урлы-коллбеки клиента на логин и логаут. Например, [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/Clients/InteractiveMvcClient.cs#L16) и [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/Clients/WebBrowserStubClient.cs#L32). Для фронтового клиента нужно сделать то же самое. По урлу-логину нужно будет написать механизм сохранения  переданного бирер-токена, по логауту - удаление
4. При необходимости добавляем внешние провайдеры авторизации [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/ExternalAuthProviders.cs#L13). Здесь упоминается гугл, но добавлять можно любые: Active Directory, Github, Gitlab, Facebook, etc. Нужно смотреть существующие библиотеки
  - Не забыть создать серкеты в гугл-консоли для использования гугловой авторизации. Есть туториалы
5. Прописать настройку IS4 в веб-приложении [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Startup.cs#L30)
6. __Optional__ При необходимости написать/добавить [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Quickstart/Account/ExternalController.cs#L93) свою логику аутентификации пользователя, который переходит после автоизации с помощью внешнего провайдера (например, гугла)
7. __Optional__ Для кастомизации выдачи клеймов сервером можно добавить свою [реализацию сервиса](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/CustomProfileService.cs#L20) IProfileService. В Startup-файле использование этого сервиса настраивается [тут](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Startup.cs#L31) и [тут](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Startup.cs#L46).

### Настройка WebApi

1. Добавляем использование аутентификации с помощью стороннего сервиса [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/WebApi/Startup.cs#L14)
2. Добавляем адентификатор скоупа [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/WebApi/Startup.cs#L27)
3. Не забываем указать использование правила авторизации скоупа [тут](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/WebApi/Startup.cs#L47)

Если к Web API сделает обращение клиент, у которого нет указанного скоупа в настройках IS4, то ему будет выдан ответ 403 самой библиотекой авторизации/аутентификации

### Настройка Machine-2-machine (m2m) клиента

1. Получаем информацию об аутентифицирующем сервисе [так](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/ClinentCli/Program.cs#L17). Здесь происходит обращение к урлу [.well-known/openid-configuration](https://docs.identityserver.io/en/latest/endpoints/discovery.html)
2. Настройка аутентифицирущих данных клиента [тут](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/ClinentCli/Program.cs#L28). Не забываем о скоупе [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/ClinentCli/Program.cs#L31)
3. [Здесь же](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/ClinentCli/Program.cs#L25) из свойства AccessToken получаем бирер-токен, который будем использовать для обращения к Web API

### Настройка Angular SPA

1. Скачиваем пакет [angular-auth-oidc-client](https://github.com/maximgorbatyuk/net-blank-app/blob/master/src/Frontend/package.json#L32)
2. [Здесь](https://github.com/maximgorbatyuk/net-blank-app/blob/master/src/Frontend/src/app/shared/services/auth/oidc-user-manager.service.ts) написан класс-обертка над авторизационным сервисом из библиотеки 
3. [Здесь](https://github.com/maximgorbatyuk/net-blank-app/blob/master/src/Frontend/src/app/shared/services/auth/auth.session.service.ts) написан класс, который работает с сессией текущего юзера и сохраняет ее в localStorage и дает возможность выдать другим компонентам инфу о текущем юзере
4. [Здесь](https://github.com/maximgorbatyuk/net-blank-app/blob/master/src/Frontend/src/app/shared/services/auth/auth.service.ts) написан класс, который дает возможность выдать другим компонентам инфу о текущем юзере
5. [Здесь](https://github.com/maximgorbatyuk/net-blank-app/blob/master/src/Frontend/src/app/components/auth-callback/auth-callback.component.ts#L21) код компонента, который обрабатываем сохранение бирер-токена после всего цикла аутентификации. Этот урл мы указываем как RedirectUris [здесь](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/IdentityConfig.cs#L52)
6. Добавляем в произвольное место кнопку Логаута на фронте: панель навигации, меню профиля, up to your decision, а в обработчике клика по кнопке пишем код вызова библиотечного кода [SignOut](https://github.com/maximgorbatyuk/net-blank-app/blob/master/src/Frontend/src/app/components/app-sidebar-menu/app-sidebar/app-sidebar.component.ts#L47)

### Настройка тестового веб-клиента [oauthdebugger.com](https://oauthdebugger.com/)

1. Добавляем [клиент авторизации](https://github.com/maximgorbatyuk/IdentityServerForAll/blob/master/src/IdentityServer/Config/Clients/WebBrowserStubClient.cs#L14)
2. Переходим по [ссылке](https://localhost:6001/connect/authorize?response_type=id_token&client_id=client&client_secret=secret&redirect_uri=https%3A%2F%2Foauthdebugger.com%2Fdebug&scope=openid%20email%20profile&nonce=wnpup8t4v2b)
3. После автризации будет показана страница с расшифровкой переданного Bearer-токена
