<template>
    <div>
        <GMapMap :center="center" :zoom="13" :options="mapOptions" map-type-id="terrain" class="bg-map">
            <GMapCluster>
                <GMapMarker
                    :key="index"
                    v-for="(m, index) in markers"
                    :position="m.position"
                    :clickable="isActive"
                    :draggable="isActive"
                    @click="center = m.position"
                />
            </GMapCluster>
        </GMapMap>
        <div id="overlay" class="max-400">
            <div class="row">
                <h2 class="col-12 title m-1">Darbības režīms</h2>
            </div>
            <div class="row m-2">
                <div class="col-12">
                    <button type="button" class="btn btn-primary btn-block w-100">
                        Izveido maršrutu caur apskates objektiem
                    </button>
                </div>
            </div>
            <div class="row m-2">
                <div class="col-12">
                    <button type="button" class="btn btn-primary btn-block w-100">
                        Parādi, kas interesants ir apkārtnē
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
            isActive: false,
            center: { lat: 56.9677, lng: 24.1056 },
            markers: [
                {
                    position: {
                        lat: 51.093048,
                        lng: 6.84212,
                    },
                }, // Along list of clusters
            ],
        };
    },
    computed: {
        mapOptions() {
            return {
                gestureHandling: this.isActive ? 'cooperative' : 'none',
                zoomControl: this.isActive,
                mapTypeControl: false,
                scaleControl: false,
                streetViewControl: false,
                rotateControl: false,
                fullscreenControl: false,
            };
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
}

.max-400 {
    max-width: 400px;
}

.bg-map {
    width: 100%;
    height: 100vh;
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
