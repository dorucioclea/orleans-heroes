import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

import { HeroClient } from "./hero.client";

@Injectable()
export class HeroService {

	constructor(
		private client: HeroClient
	) {
	}

	get(key: string): Observable<string> {
		return this.client.get(key);
	}
}