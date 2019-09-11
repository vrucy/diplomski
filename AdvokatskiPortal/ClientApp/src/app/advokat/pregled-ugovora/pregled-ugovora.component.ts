import { Cenovnik } from './../../model/Cenovnik';
import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatTabChangeEvent } from '@angular/material';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { AcceptComponent } from '../dialog/accept/accept.component';
import { PrepravitiPonuduComponent } from '../dialog/prepraviti-ponudu/prepraviti-ponudu.component';

@Component({
  selector: 'app-pregled-ugovora',
  templateUrl: './pregled-ugovora.component.html',
  styleUrls: ['./pregled-ugovora.component.css']
})
export class PregledUgovoraComponent implements OnInit {
  displayedColumns: string[] = ['ime', 'prezime', 'opis', 'button'];
  public dataSource = [];
  podatci;
  cenovnik = new Cenovnik() ;
  odgovor: string;
  constructor(private advokatService: AdvokatService, public dialog: MatDialog) { }

  openDialog(element): void {
    const dialogRef = this.dialog.open(AcceptComponent, {
      width: '250px',
      data: {odgovor: this.odgovor }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.odgovor = result;
      this.odgovor = result;
      console.log(result)
      this.advokatService.prihvatanjeSlucajaOdAdvokata(element).subscribe(res => {
    })
    });
  }
  openDialogEdit(element): void {
    const dialogRef = this.dialog.open(PrepravitiPonuduComponent, {
      width: '250px',
      // napraviti svoj cenovnik ili prepraviti postojeci???
      data: { cenovnik: this.cenovnik }
    });
    dialogRef.afterClosed().subscribe(result => {
      element.cenovnik = result;
      this.cenovnik = result;
      console.log(element);

      this.advokatService.postavljanjeNoveCeneOdAdvokata(element)
    });
  }
  ngOnInit() {
    this.advokatService.getUgovorsForAdvokat().subscribe(res => {
      this.dataSource = res;
      console.log(this.dataSource);
    });
  }

  redirectToAccept(slucajAdvokat) {

    // this.advokatService.prihvacenSlucajOdAdvokata(slucajAdvokat).subscribe(res => {
    //   console.log(res)
    // })
  }

  redirectToReject(slucajAdvokat) {
    this.advokatService.odbijanjeSlucajaOdAdvokata(slucajAdvokat).subscribe(res => {
      console.log(res)
    })
  }



  test(tabChangeEvent): void {
    if (tabChangeEvent.index === 0) {
      this.advokatService.getUgovorsForAdvokat().subscribe(res => {
        this.dataSource = res;
      });

      const a = {...this.dataSource};
      console.log(a)
      for (let i = 0; i < a.length; i++) {
        if (a[i].slucajStatusId !== 2) {
          a.splice(i);
        }


      }
     // this.dataSource = a;
      console.log(a);

    } else if (tabChangeEvent.index === 1) {
      this.advokatService.getUgovorsForAdvokat().subscribe(res => {
        this.dataSource = res;
      });

      const a = this.dataSource;

      for (let i = 0; i < a.length; i++) {
        if (a[i].slucajStatusId !== 2) {
          a.splice(i);
        }


      }
      this.dataSource = a;
      console.log(this.dataSource);

      } else {

        console.log('treci tab');

      }

    }
  }
