import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, MenubarModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'e-commerce-primeng';
}
