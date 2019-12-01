import { DodajPodKategorijuComponent } from './majstor/dodaj-pod-kategoriju/dodaj-pod-kategoriju.component';
import { PregledSlucajaKorisnikComponent } from './korisnik/pregled-slucaja-korisnik/pregled-slucaja-korisnik.component';
import { AuthGuard } from './service/authGuard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PocetnaStranicaKorisnikComponent } from './korisnik/pocetna-stranica-korisnik/pocetna-stranica-korisnik.component';
import { PocetnaStranicaMajstorComponent } from './majstor/pocetna-stranica-majstor/pocetna-stranica-majstor.component';
import { RegistarKorisnikComponent } from './register/registar-korisnik/registar-korisnik.component';
import { RegistracijaMajstoraComponent } from './majstor/registracija-majstora/registracija-majstora.component';
import { TabelaMajstoraComponent } from './korisnik/tabela-majstora/tabela-majstora.component';
import { KreiranjeSlucajaComponent } from './korisnik/kreiranje-slucaja/kreiranje-slucaja.component';
import { SlanjeSlucajaComponent } from './korisnik/slanje-slucaja/slanje-slucaja.component';
import { PregledUgovoraComponent } from './majstor/pregled-ugovora/pregled-ugovora.component';
import { PrihvacenOdgovorComponent } from './majstor/odgovoriNaPonude/prihvacen-odgovor/prihvacen-odgovor.component';
import { DodajKategorijuComponent } from './majstor/dodaj-kategoriju/dodaj-kategoriju.component';
import { NotificationComponent } from './layout/notification/notification.component';
import { EditKorisnikComponent } from './korisnik/edit-korisnik/edit-korisnik.component';
import { EditMajstorComponent } from './majstor/edit-majstor/edit-majstor.component';
import { EditSlucajComponent } from './korisnik/edit-slucaj/edit-slucaj.component';

const router: Routes = [
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'registration', component: RegistarKorisnikComponent
 },
 {
    path: 'registracijaMajstora', component: RegistracijaMajstoraComponent
 },
  {
    path: 'pocetnaMajstor', component: PocetnaStranicaMajstorComponent, canActivate: [AuthGuard]
  },
  {
    path: 'pocetnaKorisnik', component: PocetnaStranicaKorisnikComponent, canActivate: [AuthGuard]
  },
  {
    path: 'tabelaSaMajstorima', component: TabelaMajstoraComponent, canActivate: [AuthGuard]
  },
  {
    path: 'kreiranjeSlucaja', component: KreiranjeSlucajaComponent, canActivate: [AuthGuard]
  },
  {
    path: 'slanjeSlucaja', component: SlanjeSlucajaComponent, canActivate: [AuthGuard]
  },
  {
     path: '', redirectTo: '/login', pathMatch: 'full'
  },
  {
    path: 'pregledUgovora', component: PregledUgovoraComponent
  },
  {
    path: 'prihvacenUgovor' , component: PrihvacenOdgovorComponent
  },
  {
    path: 'pregledSlucajaKorisnik' , component: PregledSlucajaKorisnikComponent
  },
  {
    path: 'editKorisnik', component: EditKorisnikComponent
  },
  {
    path: 'editMajstor', component: EditMajstorComponent
  },
  {
    path: 'editSlucaj/:id', component: EditSlucajComponent, canActivate: [AuthGuard]
  },
  {
    path: 'dodajKategoriju', component: DodajKategorijuComponent
  },
  {
    path: 'dodajPodkategoriju', component: DodajPodKategorijuComponent
  },
  {
    path: 'notification' , component: NotificationComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(router)
  ],
  declarations: []
})
export class RoutingModule { }
