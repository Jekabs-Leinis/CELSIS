<template>
    <div>
        <GMapMap :center="center" :zoom="7" map-type-id="terrain" class="bg-map">
            <GMapCluster>
                <GMapMarker
                    :key="index"
                    v-for="(m, index) in markers"
                    :position="m.position"
                    :clickable="true"
                    :draggable="true"
                    @click="center = m.position"
                />
            </GMapCluster>
        </GMapMap>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    name: 'App',
    data() {
        return {
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
});
</script>

<style lang="scss">
body {
    margin: 0;
}

#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
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
