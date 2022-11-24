<template>
    <div>
        <GMapMap :center="center" :zoom="13" :options="mapOptions" map-type-id="terrain" class="bg-map" ref="gMap">
            <GMapMarker
                :key="index"
                v-for="(m, index) in markers"
                :position="m.position"
                :clickable="isActive"
                :draggable="isActive"
                @click="center = m.position"
            />
        </GMapMap>
        <!--        <div id="click-overlay" @click="processClick"></div>-->
        <div v-show="!isActive" id="overlay" class="max-400">
            <div class="row">
                <h2 class="col-12 title m-1">Darbības režīms</h2>
            </div>
            <div class="row m-2">
                <div class="col-12">
                    <button type="button" class="btn btn-primary btn-block w-100" @click="startRouting">
                        Izveido maršrutu caur apskates objektiem
                    </button>
                </div>
            </div>
            <div class="row m-2">
                <div class="col-12">
                    <button disabled type="button" class="btn btn-primary btn-block w-100">
                        Parādi, kas interesants ir apkārtnē (WIP)
                    </button>
                </div>
            </div>
        </div>
        <div v-show="isRouting" id="overlay-top" class="max-400">
            <div class="row m-2">
                <div class="col-12">
                    <h4 class="title my-1">Izvēlies punktus</h4>
                </div>
            </div>
            <div class="row m-2">
                <div class="col-12">
                    <GMapAutocomplete
                        ref="firstLoc"
                        class="form-control"
                        placeholder="Pirmais punkts"
                        @place_changed="setFirstPlace"
                    />
                </div>
            </div>
            <div class="row m-2">
                <div class="col-12">
                    <GMapAutocomplete
                        ref="secondLoc"
                        class="form-control"
                        placeholder="Otrais punkts"
                        @place_changed="setSecondPlace"
                    />
                </div>
            </div>
            <div class="row m-2">
                <div class="col-12">
                    <button
                        :disabled="!firstPoint || !secondPoint"
                        type="button"
                        class="btn btn-success btn-block w-100"
                        @click="drawRoute"
                    >
                        Izveidot maršrutu
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    name: 'App',
    data() {
        return {
            mapObject: {},
            dService: null,
            dRenderer: null,
            placeService: null,
            isActive: false,
            isRouting: false,
            enableInteraction: false,
            firstPoint: '',
            secondPoint: '',
            firstLoc: 'The Freedom Monument, Centra rajons, Rīga, LV-1050',
            secondLoc: 'TC Spice, Lielirbes iela 29, Zemgales priekšpilsēta, Rīga, LV-1046',
            center: { lat: 56.9677, lng: 24.1056 },
            markers: [],
            geoCodingApi:
                'https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyBRW7vgr-VPLDKbGVbIL9ZsqLvIoxUd5Cc&latlng=',
        };
    },
    computed: {
        mapOptions() {
            return {
                gestureHandling: this.enableInteraction ? 'cooperative' : 'none',
                zoomControl: this.enableInteraction,
                mapTypeControl: false,
                scaleControl: false,
                streetViewControl: false,
                rotateControl: false,
                fullscreenControl: false,
                styles: [
                    {
                        featureType: 'poi.business',
                        stylers: [{ visibility: 'off' }],
                    },
                    {
                        featureType: 'transit',
                        elementType: 'labels.icon',
                        stylers: [{ visibility: 'off' }],
                    },
                ],
            };
        },
    },
    mounted() {
        this.$refs.gMap.$mapPromise.then((mapObject) => {
            this.dService = new window.google.maps.DirectionsService();
            this.dRenderer = new window.google.maps.DirectionsRenderer();
            this.placeService = new window.google.maps.places.PlacesService(mapObject);
            this.mapObject = mapObject;
            this.dRenderer.setMap(mapObject);
            mapObject.addListener('click', this.handleClick);
        });
    },
    methods: {
        startRouting() {
            this.isActive = true;
            this.isRouting = true;
        },
        setFirstPlace(place) {
            this.firstPoint = place;
            this.markers[0] = {
                title: place.name,
                position: place.geometry.location,
            };
        },
        setSecondPlace(place) {
            this.secondPoint = place;
            this.markers[1] = {
                title: place.name,
                position: place.geometry.location,
            };
        },
        processClick() {
            if (!this.isRouting) {
                return;
            }

            if (!this.firstPoint) {
                this.$refs.firstLoc.$refs.input.value = this.firstLoc;
                this.firstPoint = true;
                this.markers.push({
                    position: {
                        lat: 56.95153502596555,
                        lng: 24.113319175386312,
                    },
                });
                return;
            }
            if (!this.secondPoint) {
                this.$refs.secondLoc.$refs.input.value = this.secondLoc;
                this.secondPoint = true;
                this.markers.push({
                    position: {
                        lat: 56.92978455951853,
                        lng: 24.036058601904305,
                    },
                });
                return;
            }
        },
        drawRoute() {
            const request = {
                origin: this.$refs.firstLoc.$refs.input.value,
                destination: this.$refs.secondLoc.$refs.input.value,
                waypoints: [
                    {
                        location: 'Botāniskais dārzs, Kurzeme District, Riga, Latvia',
                        stopover: true,
                    },
                ],
                travelMode: 'DRIVING',
            };

            this.dService.route(request, (result, status) => {
                if (status === 'OK') {
                    this.dRenderer.setDirections(result);
                }
            });
        },
        handleClick(e) {
            const apiUrl = `${this.geoCodingApi}${e.latLng.lat()},${e.latLng.lng()}`;

            fetch(apiUrl)
                .then((response) => response.json())
                .then((data) => {
                    if (!this.firstPoint) {
                        this.$refs.firstLoc.$refs.input.value = data.results[0].formatted_address;
                        this.firstPoint = e;
                        this.markers[0] = {
                            position: {
                                lat: e.latLng.lat(),
                                lng: e.latLng.lng(),
                            },
                        };
                    } else {
                        this.$refs.secondLoc.$refs.input.value = data.results[0].formatted_address;
                        this.secondPoint = e;
                        this.markers[1] = {
                            position: {
                                lat: e.latLng.lat(),
                                lng: e.latLng.lng(),
                            },
                        };
                    }
                });
        },
    },
});
</script>

<style lang="scss">
body {
    margin: 0;
}

.title {
    text-align: center;
    margin: 16px 20px;
}

#overlay {
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: lighten(lightgray, 10);
    border-radius: 8px;
    z-index: 200;
}

#overlay-top {
    position: fixed;
    top: 20px;
    left: 20px;
    background-color: lighten(lightgray, 10);
    border-radius: 8px;
    z-index: 200;
}

.max-400 {
    max-width: 400px;
}

.bg-map {
    width: 100%;
    height: 100vh;
}

#click-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    z-index: 100;
}

#click-overlay::after {
    content: ' ';
}

#nav {
    padding: 30px;

    a {
        font-weight: bold;
        color: #2c3e50;

        &.router-link-exact-active {
            color: #42b983;
        }
    }
}
</style>
