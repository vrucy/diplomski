import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModificationOfferComponent } from './modificartion-offer.component';

describe('ModificationOfferComponent', () => {
  let component: ModificationOfferComponent;
  let fixture: ComponentFixture<ModificationOfferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModificationOfferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModificationOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
