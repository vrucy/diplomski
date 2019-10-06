import { Majstor } from '../../model/Majstor';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { KorisnikService } from '../../service/korisnik.service';
import { SelectionModel } from '@angular/cdk/collections';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-tabela-advokata',
  templateUrl: './tabela-advokata.component.html',
  styleUrls: ['./tabela-advokata.component.css']
})
export class TabelaAdvokataComponent implements OnInit {
  displayedColumns: string[] = [ 'Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email'];
  advokati;
  kategorije;
  selection = new SelectionModel<Majstor>(true, []);
  public dataSource = new MatTableDataSource<Majstor>();
  originalData;
  podKategorije;
  selectedId;
  kat = new FormControl('');
  podK = new FormControl('');
  filter= new FormControl('');
  constructor(private korisnikService: KorisnikService) { 
    this.dataSource.filterPredicate = this.tableFilter();

   }
  filterValues = {
    kat: '',
    podK: '',
    filter:''
  };
  ngOnInit() {
    this.korisnikService.getAllAdvokati().subscribe((res: any) => {
      this.dataSource.data = res;
      this.advokati = res;
      console.log(this.advokati)
    });
    this.korisnikService.getAllKategorije().subscribe((res: any) => {
      this.originalData = res;
      this.kategorije = [...res].filter(x => !x.parentId);
    });
    this.kat.valueChanges
      .subscribe(
        kat => {
          this.filterValues.kat = kat;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
    this.podK.valueChanges
      .subscribe(
        podK => {
          this.filterValues.podK = podK;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
  }
  tableFilter(): (data: any, filter: string) => boolean {
    const filterFunction = function (data, filter): boolean {
      const searchTerms = JSON.parse(filter);
      return data.kat === <number>searchTerms.kat &&
            data.slucajStatusId === <number>searchTerms.tabIndex; 
      // return (data.ime.toLowerCase().includes(searchTerms.name) || !searchTerms.name) 
      // && data.slucajStatusId === <number>searchTerms.tabIndex;
    };
    return filterFunction;
  }
onParentChanged(evt) {
  this.setSubcategories(evt.value);
}

setSubcategories(parentId) {
  this.podKategorije = [...this.originalData].filter(x => x.parentId === parentId);
}
}
