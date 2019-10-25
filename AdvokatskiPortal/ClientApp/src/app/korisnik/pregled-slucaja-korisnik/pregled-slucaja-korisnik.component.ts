import { PrikazSlucajComponent } from './../../advokat/dialog/prikaz-slucaj/prikaz-slucaj.component';
import { PrepravitiPonuduComponent } from './../../advokat/dialog/prepraviti-ponudu/prepraviti-ponudu.component';
import { MatDialog } from '@angular/material/dialog';
import { pregledSlucajaVM } from './../../model/pregledSlucajaVM';
import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { FormControl } from '@angular/forms';
import { Cenovnik } from '../../model/Cenovnik';

@Component({
  selector: 'app-pregled-slucaja-korisnik',
  templateUrl: './pregled-slucaja-korisnik.component.html',
  styleUrls: ['./pregled-slucaja-korisnik.component.css']
})
export class PregledSlucajaKorisnikComponent implements OnInit {

  displayedColumns: string[] = ['ime', 'prezime', 'vrstaPlacanja', 'cena', 'pocetakRada', 'zavrsetakRada', 'opis', 'button'];
  public dataSource = new MatTableDataSource<pregledSlucajaVM>();

  nameFilter = new FormControl('');
  tabIndex = new FormControl('');
  cenovnik = new Cenovnik();
  sviSlucajevi: any;
  // kategorije;
  filterValues = {
    name: '',
    tabIndex: ''
  };
  odgovor: any;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private korisnikService: KorisnikService, public dialog: MatDialog) {
    this.dataSource.filterPredicate = this.tableFilter();
  }
  submitPopupForm(result) {
    // console.log('RESULTAT POPUPA', result);
    this.handleSubmitData(result);
  }

  private async handleSubmitData(result) {
    // console.log('slanje', result);
    await this.korisnikService.postavljanjeNoveCeneOdKorisnika(result);
    // await this.korisnikService.prepravkaSlucajaKorisnika(result);
  }
  openDialogEdit(element): void {
    const dialogRef = this.dialog.open(PrepravitiPonuduComponent, {
      width: '250px',
      // napraviti svoj cenovnik ili prepraviti postojeci???
      data: {
        cenovnik: Object.assign(new Cenovnik(), element),
        submitCallback: this.submitPopupForm.bind(this),
        hideUserOptions: false
      }
    });
    dialogRef.afterClosed().subscribe(async result => {
      // const cenovnik = element.cenovnici.find(c => c.majstorId === element.majstorId);
      // const cenovnik = element.cenovnici;
      // cenovnik.komentar = result.komentar;
      // cenovnik.kolicina = result.kolicina;
      // cenovnik.vrstaPlacanja = result.vrstaPlacanja;
      // this.cenovnik = result.cenovnik;
      // this.cenovnik.SlucajId = element.slucaj.id;
      // this.cenovnik.MajstorId = element.majstorId;
      // ovako cu pokusati resiti automacki refresh tabele

      // await this.korisnikService.postavljanjeNoveCeneOdKorisnika(cenovnik);
      // this.korisnikService.prepravkaSlucajaKorisnika(cenovnik);
    });
  }
  openDialogPrikazSlucaja(element): void {
    // const baseSlike = element.slucaj.slike.map(s => {
    //   s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
    //   return s;
    // });
    const dialogRef = this.dialog.open(PrikazSlucajComponent, {
      width: '40%',
      height: '70%',
      data: { naziv: element.slucaj.naziv, opis: element.slucaj.opis, slike: element.slucaj.slike }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      this.odgovor = result;
    });
  }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.korisnikService.GetAllSlucajAdvokatForKorisnik().subscribe((res: any) => {
      this.sviSlucajevi = res;
      this.remapImagesForDisplay(res);
      // console.log(this.sviSlucajevi);
      this.handleTabChange(0);
    });
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
  private remapImagesForDisplay(data) {
    data.forEach(slucaj => {
      const baseSlike = slucaj.slucaj.slike.map(s => {
        s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
        return s;
      });
    });
  }
  filter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  tableFilter(): (data: any, filter: string) => boolean {
    const filterFunction = function (data, filter): boolean {
      const searchTerms = JSON.parse(filter);
      // return (data.ime.toLowerCase().includes(searchTerms.name) || !searchTerms.name)
      return data.ime.toLowerCase().includes(searchTerms.name)

      // &&  data.slucajStatusId === <number>searchTerms.tabIndex;
    };
    return filterFunction;
  }

  // tabDirect(event) {
  //   if (event.index === 0) {
  //     this.resetFilter();

  //     this.tabIndex.setValue(1);
  //     console.log('set 1');
  //   } else if (event.index === 1) {
  //     this.resetFilter();
  //     this.tabIndex.setValue(2);

  //   } else if (event.index === 2) {
  //     this.resetFilter();
  //     this.tabIndex.setValue(6);
  //     console.log('set 3');
  //   } else if (event.index === 3) {
  //     this.resetFilter();
  //     this.tabIndex.setValue(4);
  //     console.log('set 4');
  //   }
  // }
  writeStatus(status): string {
    if (status.slucajStatusId === 2) {
      return 'prihvacen drugi advokat';
    } else {
      return 'Ceka se odgovor Advokata';
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
    const ids = { majstorId: slucaj.majstorId, slucajId: slucaj.slucaj.id }
    this.korisnikService.prihvacenSlucajOdKorisnika(ids).subscribe(res => {
      this.removeAt(slucaj);
    });
  }
  odbijenSlucaj(slucaj) {
    this.korisnikService.odbijenSlucajOdKorisnika(slucaj);
  }

  handleButton(element) {
    switch (element.slucajStatusId) {

      case 3:
        return 'Odbili ste ovu ponudu';
        break;
      case 5:
        return 'Odbijena ponuda advokata';
        break;
      case 2:
        return 'Prihvatili ste ovu ponudu';
        break;
      case 6:
        return 'Ceka se odgovor advokata';
        break;
      default:
        break;
    }
  }
  handleTabChange(tab) {
    switch (tab) {
      // filter prihvaceni
      case 0:
        this.dataSource.data = [...this.sviSlucajevi].filter(ss => ss.slucajStatusId === 2);
        break;
      // filter u procesu
      case 1:
        this.dataSource.data = [...this.sviSlucajevi].filter(ss => ss.slucajStatusId === 4 ||
          ss.slucajStatusId === 7 || ss.slucajStatusId === 1 || ss.slucajStatusId === 6);
          // || ss.slucajStatusId === 1
        break;
      // filter odbijeni
      case 2:
        this.dataSource.data = [...this.sviSlucajevi].filter(ss => ss.slucajStatusId === 5);
        break;
      case 3:
        this.dataSource.data = [...this.sviSlucajevi].filter(ss => ss.slucajStatusId === 3);
        break;
      default:
        break;
    }
    console.log(this.dataSource.data, this.sviSlucajevi);
  }
}
