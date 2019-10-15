import { SlucajSlanjeVM } from '../../model/SlucajSlanjeVM';
import { Cenovnik } from './../../model/Cenovnik';
import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatTabChangeEvent } from '@angular/material';
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
  displayedColumns: string[] = ['ime', 'prezime', 'opis', 'cena', 'button'];
  public dataSource = new MatTableDataSource<any>();
  podatci;
  nameFilter = new FormControl('');
  tabIndex = new FormControl('');
  cenovnik = new Cenovnik();
  odgovor: string;
  imageurl;
  sviSlucajevi: any;

  constructor(private advokatService: AdvokatService, public dialog: MatDialog) {
    this.advokatService.getUgovorsForAdvokat().subscribe(res => {
      // this.dataSource.data = res;
      this.sviSlucajevi = res;
      this.handleTabChange(0);


      console.log(this.dataSource.data)
    });
    this.dataSource.filterPredicate = this.tableFilter();
  }

  filterValues = {
    name: '',
    tabIndex: ''
  };

  openDialog(element): void {
    const dialogRef = this.dialog.open(AcceptComponent, {
      width: '250px',
      data: { odgovor: this.odgovor }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      this.odgovor = result;
      console.log(result);
      this.advokatService.prihvatanjeSlucajaOdAdvokata(element).subscribe(res => {
      });
    });
  }

  openDialogEdit(element): void {
    console.log(element)
    const dialogRef = this.dialog.open(PrepravitiPonuduComponent, {
      width: '250px',
      // napraviti svoj cenovnik ili prepraviti postojeci???
      // data: Object.assign(new Cenovnik(), element)
      data: Object.assign(new Cenovnik(), element)
    });
    dialogRef.afterClosed().subscribe((result: any) => {
      console.log(result);

      // element.cenovnik = result;
      // element.zavrsetakRada = result.zavrsetakRada;
      // this.cenovnik = result.cenovnik;
      // this.cenovnik.SlucajId = element.slucaj.id;
      // this.cenovnik.StatusId = element.statusId;
      // this.cenovnik.MajstorId = element.majstorId;

      // if (element.slucajStatusId === 1) {
      //   await this.advokatService.postavljanjeNoveCeneOdAdvokata(this.cenovnik);
      //   this.advokatService.prepravkaSlucajaAdvokata(element);
      // } else {
      // result.zavrsetakRada = element;
      this.advokatService.prepravkaCeneOdAdvokata(result);
      this.advokatService.prepravkaSlucajaAdvokata(result);
      // }
    });
  }
  openDialogPrikazSlucaja(element): void {

    const baseSlike = element.slucaj.slike.map(s => {
      s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
      return s;
    });
    const dialogRef = this.dialog.open(PrikazSlucajComponent, {
      width: '750px',
      height: '900px',
      data: { naziv: element.slucaj.naziv, opis: element.slucaj.opis, slike: baseSlike }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      this.odgovor = result;

      element.slucaj.slike.forEach(el => {
        // var uints = new Uint8Array(el.slikaProp);
        // var base64 = btoa(String.fromCharCode(null, uints));
        // var url = 'data:image/jpeg;base64,' + base64;
        console.log(el.slikaProp.toString());
      });

    });
  }
  tableFilter(): (data: any, filter: string) => boolean {
    let filterFunction = function (data, filter): boolean {
      let searchTerms = JSON.parse(filter);
      return data.majstor.ime.toLowerCase().indexOf(searchTerms.name) !== -1 ||
        data.slucajStatusId.toString().toLowerCase().indexOf(searchTerms.tabIndex) !== -1;
    }
    return filterFunction;
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


  tabDirect(event) {
    if (event.index === 0) {
      this.resetFilter();
      this.dataSource.filter = null;
      this.tabIndex.setValue(1);
      console.log('set 1');
    } else if (event.index === 1) {
      this.resetFilter();
      this.tabIndex.setValue(2);
      console.log('set 2');
    } else if (event.index === 2) {
      this.resetFilter();
      this.tabIndex.setValue(3);
      console.log('set 3');
    }
  }
  chekerTab(event) {
    const even = ++event.index;
    this.tabIndex.setValue(even);
    this.resetFilter();
  }
  resetFilter() {
    this.nameFilter.reset();
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
    })
  }

  redirectToReject(slucajAdvokat) {
    this.advokatService.odbijanjeSlucajaOdAdvokata(slucajAdvokat).subscribe(res => {
      console.log(res);
    });
  }
  handleTabChange(tab) {
    switch (tab.index) {
      // filter prihvaceni
      case 0:
        this.dataSource.data = [...this.sviSlucajevi].filter(ss => ss.slucajStatusId === 2);
        break;
      // filter u procesu
      case 1:
        this.dataSource.data = [...this.sviSlucajevi].filter(ss => ss.slucajStatusId === 4 ||
          ss.slucajStatusId === 7 || ss.slucajStatusId === 1);
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
        return 'Ceka se odgovor korisnika';
        break;
      default:
        break;
    }
  }
}
