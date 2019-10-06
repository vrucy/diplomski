import { RegistracijaAdvokataComponent } from './advokat/registracija-advokata/registracija-advokata.component';
import { FooterComponent } from './layout/footer/footer.component';
import { KorisnikHeaderComponent } from './layout/korisnik-header/korisnik-header.component';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RoutingModule} from './routing.module';
import { HttpErrorInterceptor } from './service/http-error.interceptor';

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
import { TabelaAdvokataComponent } from './korisnik/tabela-advokata/tabela-advokata.component';
import { AdvokatHeaderComponent } from './layout/advokat-header/advokat-header.component';
import { KreiranjeSlucajaComponent } from './korisnik/kreiranje-slucaja/kreiranje-slucaja.component';
import { SlanjeSlucajaComponent } from './korisnik/slanje-slucaja/slanje-slucaja.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { PregledUgovoraComponent } from './advokat/pregled-ugovora/pregled-ugovora.component';
import { PrihvacenOdgovorComponent } from './advokat/odgovoriNaPonude/prihvacen-odgovor/prihvacen-odgovor.component';
import { AcceptComponent } from './advokat/dialog/accept/accept.component';
import { PregledSlucajaKorisnikComponent } from './korisnik/pregled-slucaja-korisnik/pregled-slucaja-korisnik.component';
import { PrepravitiPonuduComponent } from './advokat/dialog/prepraviti-ponudu/prepraviti-ponudu.component';
import { UspesnoLogovanjeComponent } from './snackBar/uspesno-logovanje/uspesno-logovanje.component';
import { NeUspesnoLogovanjeComponent } from './snackBar/ne-uspesno-logovanje/ne-uspesno-logovanje.component';
import { PrikazSlucajComponent } from './advokat/dialog/prikaz-slucaj/prikaz-slucaj.component';
import { DodavanjeDuplogAdvokataComponent } from './snackBar/dodavanje-duplog-advokata/dodavanje-duplog-advokata.component';
import { EditProfilaComponent } from './edit-profila/edit-profila.component';
import { DodajKategorijuComponent } from './advokat/dodaj-kategoriju/dodaj-kategoriju.component';
import { DodajPodKategorijuComponent } from './advokat/dodaj-pod-kategoriju/dodaj-pod-kategoriju.component';
import { PrikazSLucajaKorisnikComponent } from './korisnik/dialog/prikaz-slucaja-korisnik/prikaz-slucaja-korisnik.component';

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
    KorisnikHeaderComponent,
    FooterComponent,
    TabelaAdvokataComponent,
    RegistracijaAdvokataComponent,
    AdvokatHeaderComponent,
    KreiranjeSlucajaComponent,
    SlanjeSlucajaComponent,
    PregledUgovoraComponent,
    PrihvacenOdgovorComponent,
    AcceptComponent,
    PregledSlucajaKorisnikComponent,
    PrepravitiPonuduComponent,
    UspesnoLogovanjeComponent,
    NeUspesnoLogovanjeComponent,
    PrikazSlucajComponent,
    DodavanjeDuplogAdvokataComponent,
    EditProfilaComponent,
    DodajKategorijuComponent,
    DodajPodKategorijuComponent,
    PrikazSLucajaKorisnikComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MaterialModule,
    FormsModule,
    RouterModule,
    RoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FlexLayoutModule
  ], entryComponents: [AcceptComponent, PrepravitiPonuduComponent , UspesnoLogovanjeComponent, NeUspesnoLogovanjeComponent,
                      PrikazSlucajComponent, DodavanjeDuplogAdvokataComponent, PrikazSLucajaKorisnikComponent ],
    providers: [AuthGuard, AuthService , {
    provide : HTTP_INTERCEPTORS,
    useClass : AuthInterceptor,
    multi : true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
