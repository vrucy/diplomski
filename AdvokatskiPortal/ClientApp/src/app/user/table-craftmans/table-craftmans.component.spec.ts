import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TableCraftmansComponent } from './table-craftmans.component';

describe('TableCraftmansComponent', () => {
  let component: TableCraftmansComponent;
  let fixture: ComponentFixture<TableCraftmansComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TableCraftmansComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TableCraftmansComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
