const pokedex = document.getElementById("pokedex");
const pokeIndex = document.getElementById("pokeIndex");


const fetchPokemon = async () => {

    const url = `https://pokeapi.co/api/v2/pokemon?limit=151`;
    const res = await fetch(url);
    const data = await res.json();
    const pokemon = data.results.map((data, index) => ({
        id: index + 1,
        image: `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${index +
            1}.png`,
    }));
    displayPokemon(pokemon);

};

const displayPokemon = (pokemon) => {

    const pokemonHTMLString = pokemon.map(pokeman => `
    <li class="card hvr-wobble-vertical" onmouseover="selectPokemon()" onClick="selectPokemon(${pokeman.id})">
        <img class="card-image" src = "${pokeman.image}"/>
    </li>
    `).join('');
    pokedex.innerHTML = pokemonHTMLString;
};

const selectPokemon = (elem) => {
    const url = `https://pokeapi.co/api/v2/pokemon/${elem}`;
    const pokemonIndex = [];
    pokemonIndex.push(fetch(url).then(res => res.json()));
    Promise.all(pokemonIndex).then(results => {
        const pokemonIndex = results.map((data) => ({
            name: data.name,
            id: data.id,
            image: data.sprites['front_default'],
            height: data.height,
            weight: data.weight,
            type: data.types.map(type =>
                type.type.name).join(','),

        }));
        displayIndex(pokemonIndex)
    });
}

const displayIndex = pokemonIndex => {
    const pokemonIndexHTMLString = pokemonIndex.map(pokeman => `
    <li class="card"><h2 class="card-title">${pokeman.id}. ${pokeman.name}</h2> <img class="card-image" src="${pokeman.image}"/>  <p class="card-subtitle">Type: ${pokeman.type}</p> <p>Height: ${pokeman.height}" </p> <p> weight: ${pokeman.weight}</li>
    `).join('');
    pokeIndex.innerHTML = pokemonIndexHTMLString;
};

fetchPokemon()