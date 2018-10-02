import randomColor from 'randomcolor';

export class NameColorProvider {
    private static cachedColors: {[name: string]: string} = {};

    public static getColor(name: string): string {
        const cachedColor = NameColorProvider.cachedColors[name];
        if (cachedColor) {
            return cachedColor;
        }
        return NameColorProvider.cachedColors[name] = randomColor({
            seed: name,
        });
    }
}
