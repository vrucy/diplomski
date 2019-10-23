import { Majstor } from '../../model/Majstor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort } from '@angular/material';
import { KorisnikService } from '../../service/korisnik.service';
import { SelectionModel } from '@angular/cdk/collections';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-tabela-advokata',
  templateUrl: './tabela-advokata.component.html',
  styleUrls: ['./tabela-advokata.component.css']
})
export class TabelaAdvokataComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email'];
  advokati;
  kategorije;
  selection = new SelectionModel<Majstor>(true, []);
  public dataSource = new MatTableDataSource<Majstor>();
  originalData;
  podKategorije;
  selectedId;
  kat = new FormControl('');
  podK = new FormControl('');
  filterTxt = new FormControl('');
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  // filter
  private cachedData: any[];
  private filteredData: any[];
  private _kategorijaValue: any;
  public set kategorijaValue(val: any) {
    this._kategorijaValue = val;
    this.filterData();
  }
  public get kategorijaValue() {
    return this._kategorijaValue;
  }
  private _podkategorijaValue: any;
  public set podkategorijaValue(val: any) {
    this._podkategorijaValue = val;
    this.filterData();
  }
  public get podkategorijaValue() {
    return this._podkategorijaValue;
  }
  private _filterInputValue: string;
  public set filterInputValue(val: any) {
    this._filterInputValue = val;
    this.filterData();
  }
  public get filterInputValue() {
    return this._filterInputValue;
  }
  // end filter
  constructor(private korisnikService: KorisnikService) {
    // this.dataSource.filterPredicate = this.tableFilter();
  }
  filterValues = {
    kat: '',
    podK: '',
    filterTxt: ''
  };
  ngOnInit() {
    this.dataSource.sort = this.sort;
    this.korisnikService.getAllAdvokati().subscribe((res: any) => {
      this.cachedData = [...res];
      this.filteredData = [...res];
      this.dataSource.data = this.filteredData;
      this.advokati = res;
      console.log(this.advokati);
    });
    this.korisnikService.getAllKategorije().subscribe((res: any) => {
      this.originalData = res;
      this.kategorije = [...res].filter(x => !x.parentId);
    });
    // this.kat.valueChanges
    //   .subscribe(
    //     kat => {
    //       this.filterValues.kat = kat;
    //       this.dataSource.filter = JSON.stringify(this.filterValues);
    //     }
    //   );
    // this.podK.valueChanges
    //   .subscribe(
    //     podK => {
    //       this.filterValues.podK = podK;
    //       this.dataSource.filter = JSON.stringify(this.filterValues);
    //     }
    //   );
    // this.filterTxt.valueChanges
    //   .subscribe(
    //     filterTxt => {
    //       this.filterValues.filterTxt = filterTxt;
    //       this.dataSource.filter = JSON.stringify(this.filterValues);
    //     }
    //   );
  }

  // tableFilter(): (data: any, filter: string) => boolean {
  //   const filterFunction = function (data, filter): boolean {
  //     const searchTerms = JSON.parse(filter);
  //     // && data.slucajStatusId === <number>searchTerms.tabIndex;
  //     let filteredResult = (
  //       data.ime.toLowerCase().includes(searchTerms.filterTxt) ||
  //       data.prezime.toLowerCase().includes(searchTerms.filterTxt) ||
  //       data.mesto.toLowerCase().includes(searchTerms.filterTxt))
  //       && data.kategorije.kategorijaId === <number>searchTerms.kat;

  //     filteredResult = filteredResult.filter(fr => fr.kategorijaId === <number>searchTerms.podK)
  //     return filteredResult;
  //     //  data.kategorije.forEach(element => {
  //     //  // tslint:disable-next-line:no-unused-expression
  //     //  element.kategorijaId === <number>searchTerms.podK;
  //     // });

  //     // && data.slucajStatusId === <number>searchTerms.tabIndex;
  //   };
  //   return filterFunction;
  // }

  filterData() {
    if (!this.cachedData) {
      return;
    }
    let filteredData = [...this.cachedData];
    // filter by name first
    if (this.filterInputValue) {
      filteredData = filteredData.filter(cd => cd.ime.includes(this.filterInputValue) || cd.prezime.includes(this.filterInputValue));
    }

    if (this.kategorijaValue) {
      filteredData = filteredData.filter(fd => fd.kategorije.find(k => k.kategorija.parentId === this.kategorijaValue.id));
    }
    if (this.podkategorijaValue) {
      filteredData = filteredData.filter(fd => fd.kategorije.find(k => k.kategorijaId === this.podkategorijaValue.id));
    }

    this.filteredData = filteredData;
    this.dataSource.data = this.filteredData;
  }
  onParentChanged(evt) {
    this.setSubcategories(evt.value);
  }

  setSubcategories(parent) {
    this.podKategorije = [...this.originalData].filter(x => x.parentId === parent.id);
  }
}
