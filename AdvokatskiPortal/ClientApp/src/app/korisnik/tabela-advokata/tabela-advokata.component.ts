import { Majstor } from '../../model/Majstor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { KorisnikService } from '../../service/korisnik.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-tabela-majstora',
  templateUrl: './tabela-majstora.component.html',
  styleUrls: ['./tabela-majstora.component.css']
})
export class TabelaMajstoraComponent implements OnInit {

  displayedColumns: string[] = [ 'ime', 'prezime', 'mesto', 'ulica', 'email'];
  majstori;
  kategorije;
  public dataSource = new MatTableDataSource<Majstor>();
  selection = new SelectionModel<Majstor>(true, []);
  originalData;
  podKategorije;
  // selectedId;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

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
  constructor(private korisnikService: KorisnikService) { }


  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    // setTimeout(() => {
    // }, 200);
    // this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;
    this.korisnikService.getAllMajstori().subscribe((res: any) => {
      this.cachedData = [...res];
      this.filteredData = [...res];
      this.dataSource.data = this.filteredData;
      this.majstori = res;
    });
      this.korisnikService.getAllKategorije().subscribe((res: any) => {
      this.originalData = res;
      this.kategorije = [...res].filter(x => !x.parentId);
    });
  }
  resetFilters(){
    this.filterInputValue = null;
    this.kategorijaValue = null;
    this.podkategorijaValue = null;
  }
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
