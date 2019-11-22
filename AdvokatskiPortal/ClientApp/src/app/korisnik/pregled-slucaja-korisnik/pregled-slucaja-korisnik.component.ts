import { element } from 'protractor';
import { PrikazSlucajComponent } from './../../advokat/dialog/prikaz-slucaj/prikaz-slucaj.component';
import { PrepravitiPonuduComponent } from './../../advokat/dialog/prepraviti-ponudu/prepraviti-ponudu.component';
import { MatDialog } from '@angular/material/dialog';
import { pregledSlucajaVM } from './../../model/pregledSlucajaVM';
import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { FormControl } from '@angular/forms';
import { Cenovnik } from '../../model/Cenovnik';

import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-pregled-slucaja-korisnik',
  templateUrl: './pregled-slucaja-korisnik.component.html',
  styleUrls: ['./pregled-slucaja-korisnik.component.css']
})
export class PregledSlucajaKorisnikComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['ime', 'prezime', 'vrstaPlacanja', 'kolicina', 'datumKreiranja'
    , 'pocetakRada', 'zavrsetakRada', 'naziv', 'button'];
  public dataSource = new MatTableDataSource();
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  nameFilter = new FormControl('');
  // tabIndex = new FormControl('');
  // cenovnik = new Cenovnik();
  sviSlucajevi: any;
  odabraniSlucaj;
  sviSlucajeviOdabir;
  private _filterInputValue: string;
  public set filterInputValue(val: any) {
    this._filterInputValue = val;
    this.filterData();
  }
  public get filterInputValue() {
    return this._filterInputValue;
  }
  private cachedData: any[];
  private filteredData: any[];
  private _slucajValue: any;
  public set slucajValue(val: any) {
    this._slucajValue = val;
    this.filterData();
  }
  public get slucajValue() {
    return this._slucajValue;
  }
  odgovor: any;

  constructor(private korisnikService: KorisnikService, public dialog: MatDialog) {
    // this.dataSource.filterPredicate = this.tableFilter();
    this.initialize();
  }
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngAfterViewInit() {
    this.dataSource.sortingDataAccessor = (item, property): any => {
      switch (property) {
        case 'pocetakRada': {
          const newDate = new Date(item.cenovnici.pocetakRada);
          return newDate;
        }
        case 'zavrsetakRada': {
          const newDate = new Date(item.cenovnici.zavrsetakRada);
          return newDate;
        }
        case 'datumKreiranja': {
          const newDate = new Date(item.cenovnici.datumKreiranja);
          return newDate;
        }
        case 'naziv': {
          return item.slucaj.naziv;
        }
        case 'kolicina': {
          return item.cenovnici.kolicina;
        }
        case 'vrstaPlacanja': {
          return item.cenovnici.vrstaPlacanja;
        }
        default: {
          return item[property];
        }
      }
    };
  }
  initialize() {
    this.korisnikService.GetAllSlucajAdvokatForKorisnik().subscribe((res: any) => {
      this.sviSlucajevi = res;
      this.cachedData = [...res];
      this.filteredData = [...res];
      this.dataSource.data = this.filteredData;

      console.log(this.dataSource.data)
      this.remapImagesForDisplay(res); 
      this.handleTabChange(0);
    });
  }
  submitPopupForm(result) {
    // this.handleSubmitData(result);
    this.korisnikService.postavljanjeNoveCeneOdKorisnika(result).subscribe(res => {
      this.initialize(); 
    });
  }

  // private handleSubmitData(result) {
  //   this.korisnikService.postavljanjeNoveCeneOdKorisnika(result).subscribe(res => {
  //     this.initialize();
  //   });
  // }
  openDialogEdit(element): void {
    const dialogRef = this.dialog.open(PrepravitiPonuduComponent, {
      width: '250px',
      data: {
        cenovnik: Object.assign(new Cenovnik(), element),
        submitCallback: this.submitPopupForm.bind(this),
        hideUserOptions: false
      }
    });

  }
  openDialogPrikazSlucaja(element): void {
    const dialogRef = this.dialog.open(PrikazSlucajComponent, {
      maxWidth: '40%',
      maxHeight: '70%',
      data: { naziv: element.slucaj.naziv, opis: element.slucaj.opis, slike: element.slucaj.slike }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      this.odgovor = result;
    });
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

  removeAt(index: number) {
    const data = this.dataSource.data;
    data.splice(index, 1);

    this.dataSource.data = data;
  }
  prihvacenSlucaj(slucaj) {
    const ids = { majstorId: slucaj.majstorId, slucajId: slucaj.id }
    this.korisnikService.prihvacenSlucajOdKorisnika(ids).subscribe(res => {
      this.removeAt(slucaj);
    });
  }
  odbijenSlucaj(slucaj) {
    console.log(slucaj)
    const ids = { majstorId: slucaj.majstorId, slucajId: slucaj.slucaj.id }
    console.log(ids)
    this.korisnikService.odbijenSlucajOdKorisnika(ids);
  }

  handleButton(element) {
    switch (element.slucajStatusId) {
      case 1:
        return 'Ceka se odgovor advokata';
        break;
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
  handleSlucaj(ids) {
    this.sviSlucajeviOdabir = [];
    ids.forEach(el => {
      const x = [...this.filteredData].find(sso => sso.slucaj.id === el);
      this.sviSlucajeviOdabir = [...this.sviSlucajeviOdabir, x.slucaj];
    });
    console.log(this.sviSlucajeviOdabir);
  }
  handleTabChange(tab) {
    switch (tab) {
      // svi
      case 0:
        console.log(tab);
        //  this.resetFilter();
        const unique0 = [...new Set(this.filteredData.map(item => item.slucaj.id))];
        this.handleSlucaj(unique0);
        this.dataSource.data = [...this.filteredData];
        break;
      // filter prihvaceni
      case 1:
        // this.resetFilter();
        const unique = [...new Set(this.filteredData.filter(ss => ss.slucajStatusId === 2).map(item => item.slucaj.id))];
        this.handleSlucaj(unique);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.slucajStatusId === 2);
        break;
      // filter u procesu
      case 2:
        // this.resetFilter();
        const unique1 = [...new Set(this.filteredData.filter(ss => ss.slucajStatusId === 4 ||
          ss.slucajStatusId === 7 || ss.slucajStatusId === 1 || ss.slucajStatusId === 6).map(item => item.slucaj.id))];
        console.log(unique1);
        this.handleSlucaj(unique1);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.slucajStatusId === 4 ||
          ss.slucajStatusId === 7 || ss.slucajStatusId === 1 || ss.slucajStatusId === 6);
        break;
      // filter odbijeni
      case 3:
        // this.resetFilter();
        const unique2 = [...new Set(this.filteredData.filter(ss => ss.slucajStatusId === 5).map(item => item.slucaj.id))];
        this.handleSlucaj(unique2);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.slucajStatusId === 5);
        break;
      case 4:
        const unique3 = [...new Set(this.filteredData.filter(ss => ss.slucajStatusId === 3).map(item => item.slucaj.id))];
        this.handleSlucaj(unique3);
        // this.resetFilter();
        this.dataSource.data = [...this.filteredData].filter(ss => ss.slucajStatusId === 3);
        break;
      default:
        break;
    }
    console.log(this.dataSource.data, this.filteredData);
  }
  resetFilter() {
    this.filterInputValue = null;
    // this.filterData().filteredData = [...this.cachedData];
    this.slucajValue = null;
    this.filterInputValue = null;
    this.sviSlucajeviOdabir = [];
  }
  public filterData() {
    if (!this.cachedData) {
      return;
    }
    let filteredData = [...this.cachedData];

    if (this.slucajValue) {
      console.log(filteredData)
      // filteredData = filteredData.filter(fd => fd.find(k => k.slucaj.id === this.slucajValue.id));
      filteredData = filteredData.filter(fd => fd.slucaj.id === this.slucajValue.id);

    }

    if (this.filterInputValue) {
      filteredData = filteredData.filter(cd => cd.ime.includes(this.filterInputValue) ||
        cd.prezime.includes(this.filterInputValue));
    }
    this.filteredData = filteredData;
    this.dataSource.data = this.filteredData;
  }
}
