import { Provider } from "@angular/core";
import { EffectsModule } from "@ngrx/effects";

import { HeroClient } from "./hero/hero.client";
import { HeroService } from "./hero/hero.service";
import { HeroSelector } from "./hero/hero.selector";
import { HeroAction } from "./hero/hero.action";
import { HeroEffect } from "./hero/hero.effect";

export { HeroClient } from "./hero/hero.client";
export { HeroService } from "./hero/hero.service";
export { HeroSelector } from "./hero/hero.selector";
export { HeroAction } from "./hero/hero.action";

export const SDK_PROVIDERS: Provider[] = [
	HeroClient,
	HeroService,
	HeroAction,
	HeroSelector
];

export const SDK_EFFECTS: any[] = [
	EffectsModule.forFeature([HeroEffect])
];