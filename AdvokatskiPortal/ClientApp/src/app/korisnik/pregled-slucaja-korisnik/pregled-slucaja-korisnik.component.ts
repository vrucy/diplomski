import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pregled-slucaja-korisnik',
  templateUrl: './pregled-slucaja-korisnik.component.html',
  styleUrls: ['./pregled-slucaja-korisnik.component.css']
})
export class PregledSlucajaKorisnikComponent implements OnInit {

  isplayedColumns: string[] = ['ime', 'prezime', 'vrstaPlacanja', 'cena', 'opis', 'button'];
  public dataSource = [];

  constructor(private korisnikService: KorisnikService) { }

  ngOnInit() {
    this.korisnikService.GetUgovorsForKorisnik().subscribe(res => {
      this.dataSource = res;
      console.log(this.dataSource);
    });
  }

  test(tabChangeEvent): void {
    if (tabChangeEvent.index === 0) {
      this.korisnikService.getAllSlucajForKorisnik().subscribe(res => {
        this.dataSource = res;
      });

      const a = {...this.dataSource};

      for (let i = 0; i < a.length; i++) {
        if (a[i].slucajStatusId !== 1) {
          a.splice(i);
        }
      }
      this.dataSource = a;
      console.log(a);

    } else if (tabChangeEvent.index === 1) {
      this.korisnikService.getSlucajNaCekanju().subscribe(res => {
        this.dataSource = res;
      });

      // const a = this.dataSource;

      // for (let i = 0; i < a.length; i++) {
      //   if (a[i].slucajStatusId !== 2) {
      //     a.splice(i);
      //   }
      //}
     // this.dataSource = a;
      console.log(this.dataSource);

      } else if (tabChangeEvent.index === 2) {
        this.korisnikService.getSlucajPrihvaceni().subscribe(res => {
          this.dataSource = res;
        });

        console.log('treci tab');

      }

    }
}
