import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { OidcClientNotification, PublicConfiguration, OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  configuration: PublicConfiguration;
  userDataChanged$: Observable<OidcClientNotification<any>>;
  userData$: Observable<any>;
  isAuthenticated$: Observable<boolean>;
  user: User;

  constructor(private oidcSecurityService: OidcSecurityService, public authService: AuthService) { }

  ngOnInit() {
    this.userData$ = this.oidcSecurityService.userData$;
    this.isAuthenticated$ = this.oidcSecurityService.isAuthenticated$;
    this.userData$.subscribe(result => {
      this.user = result as User;
    });
  }


}

interface User {
  family_name: string;
  given_name: string;
  name: string;
  sub: string;
  website: string;
}
