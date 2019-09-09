import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrihvacenOdgovorComponent } from './prihvacen-odgovor.component';

describe('PrihvacenOdgovorComponent', () => {
  let component: PrihvacenOdgovorComponent;
  let fixture: ComponentFixture<PrihvacenOdgovorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrihvacenOdgovorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrihvacenOdgovorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
