import { RegistracijaMajstoraComponent } from './majstor/registracija-majstora/registracija-majstora.component';
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
import * as _moment from 'moment';
import { AuthGuard } from './service/authGuard';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { MaterialModule} from './material.module';
import { PocetnaStranicaKorisnikComponent } from './korisnik/pocetna-stranica-korisnik/pocetna-stranica-korisnik.component';
import { PocetnaStranicaMajstorComponent } from './majstor/pocetna-stranica-majstor/pocetna-stranica-majstor.component'
import { AuthInterceptor } from './service/authInterceptor';
import { AuthService } from './service/auth.service';
import { RegistarKorisnikComponent } from './register/registar-korisnik/registar-korisnik.component';
import { TabelaMajstoraComponent } from './korisnik/tabela-majstora/tabela-majstora.component';
import { MajstorHeaderComponent } from './layout/majstor-header/majstor-header.component';
import { KreiranjeSlucajaComponent } from './korisnik/kreiranje-slucaja/kreiranje-slucaja.component';
import { SlanjeSlucajaComponent } from './korisnik/slanje-slucaja/slanje-slucaja.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { PregledUgovoraComponent } from './majstor/pregled-ugovora/pregled-ugovora.component';
import { PrihvacenOdgovorComponent } from './majstor/odgovoriNaPonude/prihvacen-odgovor/prihvacen-odgovor.component';
import { AcceptComponent } from './majstor/dialog/accept/accept.component';
import { PregledSlucajaKorisnikComponent } from './korisnik/pregled-slucaja-korisnik/pregled-slucaja-korisnik.component';
import { PrepravitiPonuduComponent } from './majstor/dialog/prepraviti-ponudu/prepraviti-ponudu.component';
import { UspesnoLogovanjeComponent } from './snackBar/uspesno-logovanje/uspesno-logovanje.component';
import { NeUspesnoLogovanjeComponent } from './snackBar/ne-uspesno-logovanje/ne-uspesno-logovanje.component';
import { PrikazSlucajComponent } from './majstor/dialog/prikaz-slucaj/prikaz-slucaj.component';
import { DodavanjeDuplogMajstoraComponent } from './snackBar/dodavanje-duplog-majstora/dodavanje-duplog-majstora.component';
import { DodajKategorijuComponent } from './majstor/dodaj-kategoriju/dodaj-kategoriju.component';
import { DodajPodKategorijuComponent } from './majstor/dodaj-pod-kategoriju/dodaj-pod-kategoriju.component';
import { PrikazSLucajaKorisnikComponent } from './korisnik/dialog/prikaz-slucaja-korisnik/prikaz-slucaja-korisnik.component';
import { NotificationComponent } from './layout/notification/notification.component';
import { MatSortModule } from '@angular/material/sort';
import { EditKorisnikComponent } from './korisnik/edit-korisnik/edit-korisnik.component';
import { EditMajstorComponent } from './majstor/edit-majstor/edit-majstor.component';
import { PrikazSlikaComponent } from './korisnik/dialog/prikaz-slika/prikaz-slika.component';
import { EditSlucajComponent } from './korisnik/edit-slucaj/edit-slucaj.component';
import { ObavestenjeKreirajSlucajComponent } from './snackBar/obavestenje-kreiraj-slucaj/obavestenje-kreiraj-slucaj.component';
import { UspesnaRegistracijaComponent } from './snackBar/uspesna-registracija/uspesna-registracija.component';
import { UspesnoStePoslaliSlucajComponent } from './snackBar/uspesno-ste-poslali-slucaj/uspesno-ste-poslali-slucaj.component';
// import { PdfViewerModule } from 'ng2-pdf-viewer';
// import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    PocetnaStranicaKorisnikComponent,
    PocetnaStranicaMajstorComponent,
    RegistarKorisnikComponent,
    KorisnikHeaderComponent,
    FooterComponent,
    TabelaMajstoraComponent,
    RegistracijaMajstoraComponent,
    MajstorHeaderComponent,
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
    DodavanjeDuplogMajstoraComponent,
    DodajKategorijuComponent,
    DodajPodKategorijuComponent,
    PrikazSLucajaKorisnikComponent,
    NotificationComponent,
    EditKorisnikComponent,
    EditMajstorComponent,
    PrikazSlikaComponent,
    EditSlucajComponent,
    ObavestenjeKreirajSlucajComponent,
    UspesnaRegistracijaComponent,
    UspesnoStePoslaliSlucajComponent
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
    MatSortModule
    // PdfViewerModule,
    // NgxExtendedPdfViewerModule

  ], entryComponents: [AcceptComponent, PrepravitiPonuduComponent , UspesnoLogovanjeComponent, NeUspesnoLogovanjeComponent,
                      PrikazSlucajComponent, DodavanjeDuplogMajstoraComponent, PrikazSLucajaKorisnikComponent, PrikazSlikaComponent,
                      ObavestenjeKreirajSlucajComponent,UspesnaRegistracijaComponent, UspesnoStePoslaliSlucajComponent],
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
  exports: [ MatSortModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
