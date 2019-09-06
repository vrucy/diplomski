import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatTabChangeEvent } from '@angular/material';

@Component({
  selector: 'app-pregled-ugovora',
  templateUrl: './pregled-ugovora.component.html',
  styleUrls: ['./pregled-ugovora.component.css']
})
export class PregledUgovoraComponent implements OnInit {
  displayedColumns: string[] = ['ime', 'prezime', 'cena', 'opis', 'button'];
  public dataSource = [];
  podatci;
  constructor(private advokatService: AdvokatService) { }

  ngOnInit() {
    this.advokatService.getUgovorsForAdvokat().subscribe(res => {
      this.dataSource = res;
      console.log(this.dataSource);
    });
  }

  redirectToAccept(slucajAdvokat) {
    console.log(slucajAdvokat.advokatId);
    this.advokatService.prihvacenSlucajAdvokat(slucajAdvokat).subscribe(res => {
      console.log(res)
    })
  }


  test(tabChangeEvent): void {
    if (tabChangeEvent.index === 0) {
      this.advokatService.getUgovorsForAdvokat().subscribe(res => {
        this.dataSource = res;
      });

      const a = this.dataSource;

      for (let i = 0; i < a.length; i++) {
        if (a[i].slucajStatusId !== 1) {
          a.splice(i);
        }


      }
      this.dataSource = a;
      console.log(this.dataSource);

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

