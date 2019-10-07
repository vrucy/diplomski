import { PrepravitiPonuduComponent } from './../../advokat/dialog/prepraviti-ponudu/prepraviti-ponudu.component';
import { MatDialog } from '@angular/material/dialog';
import { pregledSlucajaVM } from './../../model/pregledSlucajaVM';
import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { FormControl } from '@angular/forms';
import { SlucajSlanjeVM } from '../../model/SlucajSlanjeVM';
import { Cenovnik } from '../../model/Cenovnik';
import { PrikazSLucajaKorisnikComponent } from '../dialog/prikaz-slucaja-korisnik/prikaz-slucaja-korisnik.component';

@Component({
  selector: 'app-pregled-slucaja-korisnik',
  templateUrl: './pregled-slucaja-korisnik.component.html',
  styleUrls: ['./pregled-slucaja-korisnik.component.css']
})
export class PregledSlucajaKorisnikComponent implements OnInit {

  displayedColumns: string[] = ['ime', 'prezime', 'vrstaPlacanja', 'cena', 'zavrsetakRada','opis', 'button'];
  public dataSource = new MatTableDataSource<pregledSlucajaVM>();

  nameFilter = new FormControl('');
  tabIndex = new FormControl('');
  cenovnik = new Cenovnik();
  filterValues = {
    name: '',
    tabIndex: ''
  };
  constructor(private korisnikService: KorisnikService, public dialog: MatDialog) {
    this.korisnikService.GetAllSlucajAdvokatForKorisnik().subscribe(res => {
      this.dataSource.data = res;
      console.log(this.dataSource.data);
    });
    this.dataSource.filterPredicate = this.tableFilter();
  }
  openDialogEdit(element): void {
    const dialogRef = this.dialog.open(PrepravitiPonuduComponent, {
      width: '250px',
      // napraviti svoj cenovnik ili prepraviti postojeci???
      data: { cenovnik: this.cenovnik }
    });
    dialogRef.afterClosed().subscribe(async result => {
      element.cenovnik = result;
      this.cenovnik = result;
      this.cenovnik.id = element.slucaj.cenovnik.id;
      this.cenovnik.SlucajId = element.slucajId;
      this.cenovnik.StatudId = element.statusId;
      console.log(element);
      // potrebno je na klijentu onemogucuti postavljanje jos jedanput editovanje postojeceg odgovora od advokata
      // to cu postici tako sto cu staviti ngIf i proveriti status

      await this.korisnikService.postavljanjeNoveCeneOdKorisnika(this.cenovnik);
      this.korisnikService.prepravkaSlucajaKorisnika(element);
    });
  }
  openDialogPrikazSlucaja(element): void {
    const baseSlike = element.slucaj.slike.map(s => {
      s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
      return s;
    });
    const dialogRef = this.dialog.open(PrikazSLucajaKorisnikComponent, {
      width: '250px',
      data: { naziv: element.slucaj.naziv, opis: element.slucaj.opis, slike: baseSlike }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
    });
  }
  ngOnInit() {
    this.nameFilter.valueChanges
      .subscribe(
        name => {
          this.filterValues.name = name;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
    this.tabIndex.valueChanges
      .subscribe(
        id => {
          this.filterValues.tabIndex = id;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
  }
  filter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  tableFilter(): (data: any, filter: string) => boolean {
    const filterFunction = function (data, filter): boolean {
      const searchTerms = JSON.parse(filter);
      return (data.majstor.ime.toLowerCase().includes(searchTerms.name) || !searchTerms.name) &&
        data.slucajStatusId === <number>searchTerms.tabIndex;
    };
    return filterFunction;
  }

  tabDirect(event) {
    if (event.index === 0) {
      this.resetFilter();

      this.tabIndex.setValue(1);
      console.log('set 1');
    } else if (event.index === 1) {
      this.resetFilter();
      this.tabIndex.setValue(2);

    } else if (event.index === 2) {
      this.resetFilter();
      this.tabIndex.setValue(6);
      console.log('set 3');
    } else if (event.index === 3) {
      this.resetFilter();
      this.tabIndex.setValue(4);
      console.log('set 4');
    }
  }
  chekerTab(event) {
    const even = ++event.index;
    this.tabIndex.setValue(even);
    this.resetFilter();
  }
  resetFilter() {
    this.nameFilter.reset();
    this.tabIndex.reset();
  }
  removeAt(index: number) {
    const data = this.dataSource.data;
    // data.splice((this.paginator.pageIndex * this.paginator.pageSize) + index, 1);
     data.splice(index, 1);

    this.dataSource.data = data;
  }
  prihvacenSlucaj(slucaj) {
    this.korisnikService.prihvacenSlucajOdKorisnika(slucaj).subscribe(res => {
      this.removeAt(slucaj);
    });
  }
  odbijenSlucaj(slucaj) {
    this.korisnikService.odbijenSlucajOdKorisnika(slucaj);
  }

  getLastOffer(element: any) {
    let result = null;
    if (element.slucaj.cenovniks.length > 0) {
      result = element.slucaj.cenovniks[element.slucaj.cenovniks.length - 1];
      // result = element[length - 1];
    }
   // console.log(element.slucaj.cenovniks);
    return  element.slucaj.cenovniks[element.slucaj.cenovniks.length - 1];
  }
}
