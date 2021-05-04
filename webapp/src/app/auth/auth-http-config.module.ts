import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';
import { environment } from './../../environments/environment';

export function configureAuth(oidcConfigService: OidcConfigService) {
    return () =>
      oidcConfigService.withConfig({
        stsServer: `${window.location.origin}/identity`,
        redirectUrl: `${window.location.origin}/`,
        postLogoutRedirectUri: `${window.location.origin}/`,
        clientId: 'angular-webapp',
        scope: 'openid profile weatherdata.read locationdata.read',
        responseType: 'code',
        silentRenew: true,
        silentRenewUrl: `${window.location.origin}/silent-renew.html`,
        renewTimeBeforeTokenExpiresInSeconds: 10,
        logLevel: environment.production ? LogLevel.None : LogLevel.Debug,
      });
}

@NgModule({
    imports: [AuthModule.forRoot()],
    providers: [
        OidcConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: configureAuth,
            deps: [OidcConfigService],
            multi: true,
        },
    ],
    exports: [AuthModule],
})
export class AuthHttpConfigModule {}
