import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RoutingModule} from './routing.module'

import { AuthGuard } from './service/authGuard';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { MaterialModule} from './material.module';
import { PocetnaStranicaKorisnikComponent } from './korisnik/pocetna-stranica-korisnik/pocetna-stranica-korisnik.component';
import { PocetnaStranicaAdvokatComponent } from './advokat/pocetna-stranica-advokat/pocetna-stranica-advokat.component'
import { AuthInterceptor } from './service/authInterceptor';
import { AuthService } from './service/auth.service';
import { RegistarKorisnikComponent } from './register/registar-korisnik/registar-korisnik.component';
import { LayoutKorisnikComponent } from './korisnik/layout-korisnik/layout-korisnik.component';
import { FooterComponent } from './footer/footer.component';
import { KorisnikHeaderComponent } from './layput/korisnik-header/korisnik-header.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    PocetnaStranicaKorisnikComponent,
    PocetnaStranicaAdvokatComponent,
    RegistarKorisnikComponent,
    LayoutKorisnikComponent,
    FooterComponent,
    KorisnikHeaderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MaterialModule,
    FormsModule,
    RouterModule,
    RoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [AuthGuard,AuthService , {
    provide : HTTP_INTERCEPTORS,
    useClass : AuthInterceptor,
    multi : true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
