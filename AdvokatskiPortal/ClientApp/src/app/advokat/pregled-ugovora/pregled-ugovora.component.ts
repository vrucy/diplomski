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
  public dataSource = new MatTableDataSource<SlucajSlanjeVM>();
  podatci;
  nameFilter = new FormControl('');
  tabIndex = new FormControl('');
  cenovnik = new Cenovnik();
  odgovor: string;
  constructor(private advokatService: AdvokatService, public dialog: MatDialog) {
    this.advokatService.getUgovorsForAdvokat().subscribe(res => {
      this.dataSource.data = res;
      console.log(this.dataSource.data);
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
      console.log(result)
      this.advokatService.prihvatanjeSlucajaOdAdvokata(element).subscribe(res => {
      });

    });
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
      console.log(element);
      // potrebno je na klijentu onemogucuti postavljanje jos jedanput editovanje postojeceg odgovora od advokata
      // to cu postici tako sto cu staviti ngIf i proveriti status
      await this.advokatService.postavljanjeNoveCeneOdAdvokata(element);
      this.advokatService.prepravkaSlucajaAdvokata(element);
    });
  }
  openDialogPrikazSlucaja(element): void {
    const dialogRef = this.dialog.open(PrikazSlucajComponent, {
      width: '250px',
      data: { naziv: element.slucaj.naziv, opis: element.slucaj.opis }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      this.odgovor = result;
      console.log(result)


    });
  }
  tableFilter(): (data: any, filter: string) => boolean {
    let filterFunction = function (data, filter): boolean {
      let searchTerms = JSON.parse(filter);
      return data.advokat.ime.toLowerCase().indexOf(searchTerms.name) !== -1 ||
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
  redirectToAccept(slucajAdvokat) {

    this.advokatService.prihvatanjeSlucajaOdAdvokata(slucajAdvokat).subscribe(res => {
      console.log(res);
    })
  }

  redirectToReject(slucajAdvokat) {
    this.advokatService.odbijanjeSlucajaOdAdvokata(slucajAdvokat).subscribe(res => {
      console.log(res);
    });
  }
}
