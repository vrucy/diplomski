import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NeUspesnoLogovanjeComponent } from './ne-uspesno-logovanje.component';

describe('NeUspesnoLogovanjeComponent', () => {
  let component: NeUspesnoLogovanjeComponent;
  let fixture: ComponentFixture<NeUspesnoLogovanjeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NeUspesnoLogovanjeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NeUspesnoLogovanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
