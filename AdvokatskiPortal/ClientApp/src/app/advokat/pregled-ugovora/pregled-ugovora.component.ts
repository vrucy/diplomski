import { SlucajSlanjeVM } from '../../model/SlucajSlanjeVM';
import { Cenovnik } from './../../model/Cenovnik';
import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTabChangeEvent, MatPaginator, MatSort } from '@angular/material';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AcceptComponent } from '../dialog/accept/accept.component';
import { PrepravitiPonuduComponent } from '../dialog/prepraviti-ponudu/prepraviti-ponudu.component';
import { FormControl } from '@angular/forms';
import { PrikazSlucajComponent } from '../dialog/prikaz-slucaj/prikaz-slucaj.component';

@Component({
  selector: 'app-pregled-ugovora',
  templateUrl: './pregled-ugovora.component.html',
  styleUrls: ['./pregled-ugovora.component.css']
})
export class PregledUgovoraComponent implements OnInit {
  displayedColumns: string[] = ['ime', 'prezime', 'opis', 'cena', 'najkasnijiOdgovor' , 'button'];
  public dataSource = new MatTableDataSource<any>();
  podatci;
  nameFilter = new FormControl('');
  tabIndex = new FormControl('');
  cenovnik = new Cenovnik();
  odgovor: string;
  imageurl;
  sviSlucajevi: any;
  sviSlucajeviOdabir;
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
  private _filterInputValue: string;
  public set filterInputValue(val: any) {
    this._filterInputValue = val;
    this.filterData();
  }
  public get filterInputValue() {
    return this._filterInputValue;
  }
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private advokatService: AdvokatService, public dialog: MatDialog) {
    this.initialize();
    // this.dataSource.filterPredicate = this.tableFilter();
  }
  // filterValues = {
  //   name: '',
  //   tabIndex: ''
  // };

  initialize() {
    this.advokatService.getUgovorsForAdvokat().subscribe(res => {
      // this.dataSource.data = res;
      this.sviSlucajevi = res;
      this.cachedData = [...res];
      this.filteredData = [...res];
      this.dataSource.data = this.filteredData;
      this.sviSlucajevi.map(ss => {
        ss.slucaj.slike.map(s => {
          s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
          return s;
        });
        return ss;
      });
      this.handleTabChange(0);
    });
  }

  openDialogEdit(element): void {
    console.log(element)
    const dialogRef = this.dialog.open(PrepravitiPonuduComponent, {
      width: '250px',
      data: {
        cenovnik: Object.assign(new Cenovnik(), element),
        submitCallback: this.submitPopupForm.bind(this),
        hideUserOptions: true,
        zavrsetakRada: element.zavrsetakRada,
        pocetakRada: element.pocetakRada
      }
    });
  }
  submitPopupForm(result) {
    console.log('RESULTAT POPUPA', result)
     this.advokatService.prepravkaSlucajaAdvokata(result).subscribe(res => {
     this.initialize() ;
     });
  }
  openDialogPrikazSlucaja(element): void {
    const dialogRef = this.dialog.open(PrikazSlucajComponent, {
      width: '750px',
      height: '900px',
      data: { naziv: element.slucaj.naziv, opis: element.slucaj.opis, slike: element.slucaj.slike }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      this.odgovor = result;
    });
  }
  // tableFilter(): (data: any, filter: string) => boolean {
  //   const filterFunction = function (data, filter): boolean {
  //     const searchTerms = JSON.parse(filter);
  //     return data.majstor.ime.toLowerCase().indexOf(searchTerms.name) !== -1 ||
  //       data.slucajStatusId.toString().toLowerCase().indexOf(searchTerms.tabIndex) !== -1;
  //   }
  //   return filterFunction;
  // }
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    // this.nameFilter.valueChanges
    //   .subscribe(
    //     name => {
    //       this.filterValues.name = name;
    //       this.dataSource.filter = JSON.stringify(this.filterValues);
    //     }
    //   );
    // this.tabIndex.valueChanges
    //   .subscribe(
    //     id => {
    //       this.filterValues.tabIndex = id;
    //       this.dataSource.filter = JSON.stringify(this.filterValues);
    //     }
    //   );

  }
  removeAt(index: number) {
    const data = this.dataSource.data;
    // data.splice((this.paginator.pageIndex * this.paginator.pageSize) + index, 1);
    data.splice(index, 1);

    this.dataSource.data = data;
  }
  redirectToAccept(slucajAdvokat) {
    this.advokatService.prihvatanjeSlucajaOdAdvokata(slucajAdvokat).subscribe(res => {
      console.log(res);
      this.removeAt(slucajAdvokat);
    });
  }

  redirectToReject(slucajMajstor) {
    const ids = {majstorId: slucajMajstor.majstorId , slucajId: slucajMajstor.slucaj.id}
      this.advokatService.odbijanjeSlucajaOdAdvokata(ids).subscribe((res: any) => {
        console.log(res);
      });
  }
  handleSlucaj(ids) {
    this.sviSlucajeviOdabir = [];
    ids.forEach(el => {
      const x = [...this.filteredData].find(sso => sso.slucaj.id === el);
      this.sviSlucajeviOdabir.push(x.slucaj);
    });
    console.log(this.sviSlucajeviOdabir);
  }
  handleTabChange(tab) {
    switch (tab) {
      case 0:
          const unique0 = [...new Set(this.filteredData.map(item => item.slucaj.id))];
          this.handleSlucaj(unique0);
        this.dataSource.data = [...this.filteredData];
        break;
      // filter prihvaceni
      case 1:
          const unique = [...new Set(this.filteredData.filter(ss => ss.slucajStatusId === 2).map(item => item.slucaj.id))];
          this.handleSlucaj(unique);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.slucajStatusId === 2);
        break;
      // filter u procesu
      case 2:
          const unique1 = [...new Set(this.filteredData.filter(
                        ss => ss.slucajStatusId === 6 || ss.slucajStatusId === 1 || ss.slucajStatusId === 7).map(item => item.slucaj.id))];
           console.log(unique1);
          this.handleSlucaj(unique1);
        this.dataSource.data = [...this.filteredData].filter(ss =>
          ss.slucajStatusId === 6 || ss.slucajStatusId === 1 || ss.slucajStatusId === 7);
        break;
      // filter odbijeni
      case 3:
          const unique2 = [...new Set(this.filteredData.filter(ss => ss.slucajStatusId === 5).map(item => item.slucaj.id))];
          this.handleSlucaj(unique2);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.slucajStatusId === 5);
        break;
      case 4:
          const unique3 = [...new Set(this.filteredData.filter(ss => ss.slucajStatusId === 3).map(item => item.slucaj.id))];
          this.handleSlucaj(unique3);
        this.dataSource.data = [...this.filteredData].filter(ss => ss.slucajStatusId === 3);
        break;
      default:
        break;
    }
    console.log(this.dataSource.data, this.filteredData);
  }
  handleButton(element) {
    switch (element.slucajStatusId) {
      // case 1:
      //   return 'Ceka se odgovor korisnika';
      //   break;
      case 2:
        return 'Korisnik je prihvatio';
        break;
      case 3:
        return 'Korisnik je odbio ponudu';
        break;
      case 7:
        return 'Ceka se odgovor klijanta';
        break;
      case 5:
        return 'Odbili ste ponudu';
      default:
        break;
    }
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
      filteredData = filteredData.filter(cd => cd.ime.includes(this.filterInputValue) || cd.prezime.includes(this.filterInputValue));
    }
    this.filteredData = filteredData;
    this.dataSource.data = this.filteredData;
  }
}

